
namespace Microsoft.Samples.Kinect.DepthBasics
{
    using System;
    using System.IO;
    using Microsoft.Kinect;

    public class StretchCar
    {
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Intermediate storage for the depth data received from the camera
        /// </summary>
        private DepthImagePixel[] depthPixels;

        /// <summary>
        /// Intermediate storage for the depth data converted to color
        /// </summary>
        private byte[] colorPixels;

        private static int SENSITIVE_DEPTH = 850;

        private Monitor monitor;

        private Windshield gameWindow;

        private Dashboard dashboard;

        private static int SCREEN_WIDTH = 640;
        private static int SCREEN_HEIGHT = 480;

        private static int STEER_X = 80;
        private static int STEER_Y = 80;
        private static int STEER_RADIUS = 78;

        private static int HORN_X1 = 162;
        private static int HORN_Y1 = 0;
        private static int HORN_X2 = 318;
        private static int HORN_Y2 = 0;
        private static int HORN_X3 = 240;
        private static int HORN_Y3 = 160;

        private static int WINDSHIELD_WIPER_X1 = 322;
        private static int WINDSHIELD_WIPER_Y1 = 0;
        private static int WINDSHIELD_WIPER_X2 = 478;
        private static int WINDSHIELD_WIPER_Y2 = 160;

        private static int ANIMAL_BUTTON_X = 560;
        private static int ANIMAL_BUTTON_Y = 80;
        private static int ANIMAL_BUTTON_RADIUS = 78;

        public StretchCar()
        {
            // reverseItems();
            gameWindow = new Windshield();
            dashboard = new Dashboard();
            dashboard.addItem(new SteeringWheel(STEER_X, STEER_Y, STEER_RADIUS));
            dashboard.addItem(new Horn(HORN_X1, HORN_Y1, HORN_X2, HORN_Y2, HORN_X3, HORN_Y3));
            dashboard.addItem(new AnimalButton(ANIMAL_BUTTON_X, ANIMAL_BUTTON_Y, ANIMAL_BUTTON_RADIUS));
            dashboard.addItem(new WindshieldWiper(WINDSHIELD_WIPER_X1, WINDSHIELD_WIPER_Y1, WINDSHIELD_WIPER_X2, WINDSHIELD_WIPER_Y2));

            this.setup();

            monitor = new Monitor(this.sensor);
            monitor.Show();
            gameWindow.Show();
        }

        private void reverseItems()
        {
            reverse(ref STEER_X);
            reverse(ref HORN_X1);
            reverse(ref HORN_X2);
            reverse(ref HORN_X3);
            reverse(ref WINDSHIELD_WIPER_X1);
            reverse(ref WINDSHIELD_WIPER_X2);
            reverse(ref ANIMAL_BUTTON_X);
        }

        private void reverse(ref int x)
        {
            x = SCREEN_WIDTH - x;
        }

        private void setup()
        {
            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the depth stream to receive depth frames
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

                // Allocate space to put the depth pixels we'll receive
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];

                // Allocate space to put the color pixels we'll create
                this.colorPixels = new byte[this.sensor.DepthStream.FramePixelDataLength * sizeof(int)];


                // Add an event handler to be called whenever there is new depth frame data
                this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's DepthFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorDepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame != null)
                {
                    // Copy the pixel data from the image to a temporary array
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);

                    // Get the min and max reliable depth for the current frame
                    int minDepth = depthFrame.MinDepth;
                    int maxDepth = depthFrame.MaxDepth;

                    // Convert the depth to RGB
                    int colorPixelIndex = 0;
                    int min = maxDepth;
                    int index = 0;
                    int iDepth = 0;
                    for (int i = 0; i < this.depthPixels.Length; ++i)
                    {
                        // Get the depth for this pixel
                        short depth = depthPixels[i].Depth;

                        // To convert to a byte, we're discarding the most-significant
                        // rather than least-significant bits.
                        // We're preserving detail, although the intensity will "wrap."
                        // Values outside the reliable depth range are mapped to 0 (black).

                        // Note: Using conditionals in this loop could degrade performance.
                        // Consider using a lookup table instead when writing production code.
                        // See the KinectDepthViewer class used by the KinectExplorer sample
                        // for a lookup table example.
                        byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                        // Write out blue byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out green byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out red byte                        
                        this.colorPixels[colorPixelIndex++] = intensity;

                        if (depth < min && depth >= minDepth)
                        {
                            min = depth;
                            index = i;
                            iDepth = depthPixels[i].Depth;
                        }

                        // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                        // If we were outputting BGRA, we would write alpha here.
                        ++colorPixelIndex;
                    }

                    // Console.WriteLine("val: " + index + "\tx-coord: " + index % 640 + "\ty-coord: " + index / 640 + "\tdepth: " + iDepth);
                    if (iDepth < SENSITIVE_DEPTH)
                    {
                        DashboardItem item = dashboard.findItem(index % 640, index / 640);
                        if (item is SteeringWheel)
                        {
                            gameWindow.steerPressed();
                        }
                        else if (item is Horn)
                        {
                            gameWindow.hornPressed();
                        }
                        else if (item is AnimalButton)
                        {
                            gameWindow.animalButtonPressed();
                        }
                        else if (item is WindshieldWiper)
                        {
                            gameWindow.windshieldWiperPressed();
                        }
                        else
                        {
                            gameWindow.noPress();
                        }
                    }
                    else
                    {
                        gameWindow.noPress();
                    }

                    gameWindow.setAudio();


                    this.drawCircle(STEER_X, STEER_Y, STEER_RADIUS);
                    this.drawRectangle(WINDSHIELD_WIPER_X1, WINDSHIELD_WIPER_Y1, WINDSHIELD_WIPER_X2, WINDSHIELD_WIPER_Y2);
                    this.drawTriangle(HORN_X1, HORN_Y1, HORN_X2, HORN_Y2, HORN_X3, HORN_Y3);
                    this.drawCircle(ANIMAL_BUTTON_X, ANIMAL_BUTTON_Y, ANIMAL_BUTTON_RADIUS);

                    this.monitor.paintBitmap(index, iDepth, this.colorPixels, iDepth <= SENSITIVE_DEPTH);


                }
            }
        }

        private void paintRed(int x, int y)
        {
            for (int i = x - 7; i < x + 7; ++i)
            {
                for (int j = y - 7; j < y + 7; ++j)
                {
                    if (i >= 0 && i < 640 && j >= 0 && j < 480)
                    {
                        colorPixels[(j * 640 + i) * 4] = (byte)0;
                        colorPixels[(j * 640 + i) * 4 + 1] = (byte)0;
                        colorPixels[(j * 640 + i) * 4 + 2] = (byte)255;
                    }
                }
            }
        }

        private void drawCircle(int xCoord, int yCoord, int radius)
        {
            if (xCoord - radius < 0 || xCoord + radius >= SCREEN_WIDTH || yCoord - radius < 0 || yCoord + radius >= SCREEN_HEIGHT)
            {
                return;
            }
            for (int i = 0; i <= radius; ++i)
            {
                int d = (int)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(i, 2));
                int x = xCoord + i;
                int y = yCoord + d;
                paintRed(x, y);
                y = yCoord - d;
                paintRed(x, y);
                x = xCoord - i;
                paintRed(x, y);
                y = yCoord + d;
                paintRed(x, y);
            }

        }

        private void drawRectangle(int x1, int y1, int x2, int y2)
        {
            if (x1 < 0 || x1 >= SCREEN_WIDTH || x2 < 0 || x2 >= SCREEN_WIDTH ||
                y1 < 0 || y1 >= SCREEN_HEIGHT || y2 < 0 || y2 >= SCREEN_HEIGHT)
            {
                return;
            }
            for (int i = (x1 < x2 ? x1 : x2); i <= (x1 > x2 ? x1 : x2); ++i)
            {
                paintRed(i, y1);
                paintRed(i, y2);
            }
            for (int j = (y1 < y2 ? y1 : y2); j <= (y1 > y2 ? y1 : y2); ++j)
            {
                paintRed(x1, j);
                paintRed(x2, j);
            }

        }

        private void drawTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if (x1 < 0 || x1 >= SCREEN_WIDTH || x2 < 0 || x2 >= SCREEN_WIDTH ||
                x3 < 0 || x3 >= SCREEN_WIDTH || y1 < 0 || y1 >= SCREEN_HEIGHT ||
                y2 < 0 || y2 >= SCREEN_HEIGHT || y3 < 0 || y3 >= SCREEN_HEIGHT)
            {
                return;
            }
            for (int i = (x1 < x2 ? x1 : x2); i <= (x1 > x2 ? x1 : x2); ++i)
            {
                int y = y1 + (i - x1) * (y2 - y1) / (x2 - x1);
                paintRed(i, y);
            }
            for (int i = (x2 < x3 ? x2 : x3); i <= (x2 > x3 ? x2 : x3); ++i)
            {
                int y = y2 + (i - x2) * (y3 - y2) / (x3 - x2);
                paintRed(i, y);
            }
            for (int i = (x1 < x3 ? x1 : x3); i <= (x1 > x3 ? x1 : x3); ++i)
            {
                int y = y1 + (i - x1) * (y3 - y1) / (x3 - x1);
                paintRed(i, y);
            }
        }

    }

}