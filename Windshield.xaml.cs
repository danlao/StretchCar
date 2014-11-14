using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Collections;

namespace Microsoft.Samples.Kinect.DepthBasics
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Windshield : Window
    {

        enum SceneStatus
        {
            Still,
            Moving,
            Rain,
            AnimalShowing,
            AnimalStill,
            AnimalLeaving
        };

        SceneStatus sceneStatus;
        Animation animation;
        ArrayList animations;

        private string rootpath = System.AppDomain.CurrentDomain.BaseDirectory;

        public Windshield()
        {
            InitializeComponent();

            this.animation = new JungleAnimation();
            this.sceneStatus = SceneStatus.Still;
            this.animation.carStops(this.GUI);

            //soundMediaElement.Source = new Uri(this.animation.getAudioPath());
            //soundMediaElement.Play();
        }

        public void steerPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.carMoves(this.GUI);
                sceneStatus = SceneStatus.Moving;
            }

            Console.WriteLine("steering wheel pressed");
        }

        public void hornPressed()
        {
            if (sceneStatus == SceneStatus.AnimalShowing || sceneStatus == SceneStatus.AnimalStill)
            {
                this.animation.animalLeaves(this.GUI);
                sceneStatus = SceneStatus.AnimalLeaving;
                Thread thread = new Thread(handleAnimalLeavingTransition);
                thread.Start();
            }

            Console.WriteLine("horn pressed");
        }

        public void windshieldWiperPressed()
        {
            // TODO: implement this
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.startRain(this.GUI);
                sceneStatus = SceneStatus.Rain;
                Thread thread = new Thread(handleRainingTransition);
                thread.Start();
            }
        }

        public void animalButtonPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.animalAppears(this.GUI);
                sceneStatus = SceneStatus.AnimalShowing;
                Thread thread = new Thread(handleAnimalShowingTransition);
                thread.Start();
            }
        }

        public void noPress()
        {
            if (sceneStatus == SceneStatus.Moving)
            {
                this.animation.carStops(this.GUI);
                sceneStatus = SceneStatus.Still;
            }
        }

        public void switchEnvironment()
        {
            this.animation = (Animation)this.animations[(this.animations.IndexOf(this.animation) + 1) % this.animations.Count];
            //soundMediaElement.Close();
            //soundMediaElement.Source = new Uri(this.animation.getAudioPath());
            //soundMediaElement.Play();
        }

        public void handleAnimalShowingTransition()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.Milliseconds > 3000)
                {
                    if (sceneStatus == SceneStatus.AnimalShowing)
                    {
                        this.animation.animalStill(this.GUI);
                        sceneStatus = SceneStatus.AnimalStill;
                    }
                    break;
                }
            }
        }

        public void handleAnimalLeavingTransition()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.Milliseconds > 3000)
                {
                    if (sceneStatus == SceneStatus.AnimalLeaving)
                    {
                        this.animation.carStops(this.GUI);
                        sceneStatus = SceneStatus.Still;
                    }
                    break;
                }
            }
        }


        public void handleRainingTransition()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.Milliseconds > 9000)
                {
                    if (sceneStatus == SceneStatus.Rain)
                    {
                        this.animation.carStops(this.GUI);
                        sceneStatus = SceneStatus.Still;
                    }
                    break;
                }
                else if (d.Milliseconds > 6000)
                {
                    if (sceneStatus == SceneStatus.Rain)
                    {
                        this.animation.endRain(this.GUI);
                    }
                    break;
                }
                else if (d.Milliseconds > 3000)
                {
                    if (sceneStatus == SceneStatus.Rain)
                    {
                        this.animation.Raining(this.GUI);
                    }
                    break;
                }
            }
        }
    }
}
