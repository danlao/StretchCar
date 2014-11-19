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
            Raining,
            AnimalShowing,
            AnimalStill,
            AnimalLeaving
        };

        SceneStatus sceneStatus;
        Animation animation;
        ArrayList animations;

		TimeSpan steerTime = TimeSpan.Zero;

        private string rootpath = System.AppDomain.CurrentDomain.BaseDirectory;

        public Windshield()
        {
			InitializeComponent();

			this.animations = new ArrayList();
			this.animations.Add(new JungleAnimation());
			this.animations.Add(new CountrysideAnimation());

            this.animation = this.animations[0] as Animation;
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
        }

        public void hornPressed()
        {
            // TODO play honk sound
            if (sceneStatus == SceneStatus.AnimalStill)
            {
                this.animation.animalLeaves(this.GUI);
                sceneStatus = SceneStatus.AnimalLeaving;
                Thread thread = new Thread(handleAnimalLeavingTransition);
                thread.Start();
            }
        }

        public void windshieldWiperPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.Raining(this.GUI);
                sceneStatus = SceneStatus.Raining;
                Thread thread = new Thread(handleRainingTransition);
                thread.Start();
            }
        }

        public void animalButtonPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.rollAnimal();
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
            this.animation.carStops(this.GUI);
			this.steerTime = TimeSpan.Zero;
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
                if (d.TotalMilliseconds > 3000)
                {
                    if (sceneStatus == SceneStatus.AnimalShowing)
                    {
                        this.Dispatcher.Invoke((Action)delegate()
                        {
                            this.animation.animalStill(this.GUI);
                        });
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
                if (d.TotalMilliseconds > 3000)
                {
                    if (sceneStatus == SceneStatus.AnimalLeaving)
                    {
                        this.Dispatcher.Invoke((Action)delegate()
                        {
                            this.animation.carStops(this.GUI);
                        });
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
                if (d.TotalMilliseconds > 9000 && sceneStatus == SceneStatus.Raining)
                {
                    this.Dispatcher.Invoke((Action)delegate()
                    {
                        this.animation.carStops(this.GUI);
                    });
                    sceneStatus = SceneStatus.Still;

                    break;
                }
            }
        }

		public void updateSteerTime(TimeSpan timeVal)
		{
			steerTime += timeVal;
		}

		public TimeSpan getSteerTime()
		{
			return steerTime;
		}
    }
}
