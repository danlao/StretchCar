
namespace Microsoft.Samples.Kinect.DepthBasics
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Monitor : Window
    {
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap colorBitmap;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public Monitor(KinectSensor sensor)
        {
            InitializeComponent();
            this.sensor = sensor;
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {

            if (null != this.sensor)
            {

                // This is the bitmap we'll display on-screen
                this.colorBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

                // Set the image we display to point to the bitmap where we'll put the image data
                this.Image.Source = this.colorBitmap;

            }

            if (null == this.sensor)
            {
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

        public void paintBitmap(int index, int depth, byte[] colorPixels, bool sensitive)
        {
            lblDepth.Content = "val: " + index + "\tx-coord: " + index % 640 + "\ty-coord: " + index / 640 + "\tdepth: " + depth;

            for (int i = index % 640 - 10; i < index % 640 + 10; ++i)
            {
                for (int j = index / 640 - 10; j < index / 640 + 10; ++j)
                {
                    if (i >= 0 && i < 640 && j >= 0 && j < 480)
                    {
                        colorPixels[(j * 640 + i) * 4] = (byte)(sensitive ? 0 : 255);
                        colorPixels[(j * 640 + i) * 4 + 1] = (byte)(sensitive ? 255 : 0);
                        colorPixels[(j * 640 + i) * 4 + 2] = (byte)0;
                    }
                }
            }

            this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);
        }

        /// <summary>
        /// Handles the user clicking on the screenshot button
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ButtonScreenshotClick(object sender, RoutedEventArgs e)
        {
            if (null == this.sensor)
            {
                return;
            }

            // create a png bitmap encoder which knows how to save a .png file
            BitmapEncoder encoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            encoder.Frames.Add(BitmapFrame.Create(this.colorBitmap));

            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            // write the new file to disk
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fs);
                }

            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// Handles the checking or unchecking of the near mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxNearModeChanged(object sender, RoutedEventArgs e)
        {
            if (this.sensor != null)
            {
                this.sensor.DepthStream.Range = DepthRange.Default;
            }
        }

        public KinectSensor kinectSensor
        {
            set
            {
                this.sensor = value;
            }
            get
            {
                return this.sensor;
            }
        }

    }
}