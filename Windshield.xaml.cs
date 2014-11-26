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
using System.Windows.Threading;

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

		private MediaPlayer honkAudio;

        public Windshield()
        {
			InitializeComponent();

			this.animations = new ArrayList();
			this.animations.Add(new JungleAnimation());
			this.animations.Add(new CountrysideAnimation());

            this.animation = this.animations[0] as Animation;
            this.sceneStatus = SceneStatus.Still;
            this.animation.carStops(this.GUI);
            soundMediaElement.Source = new Uri(this.animation.getDrivingAudioPath());
            //soundMediaElement.Play();

			honkAudio = new MediaPlayer();
			honkAudio.Open(new Uri(this.animation.getHonkAudioPath()));
        }

        public void steerPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.carMoves(this.GUI);
                sceneStatus = SceneStatus.Moving;
                this.soundMediaElement.Source = new Uri(this.animation.getDrivingAudioPath());
                this.soundMediaElement.Play();
            }
        }

        public void hornPressed()
        {
            // TODO play honk sound
			if (sceneStatus == SceneStatus.AnimalStill)
			{
				double duration = this.animation.animalLeaves(this.GUI);
				sceneStatus = SceneStatus.AnimalLeaving;
				this.soundMediaElement.Source = new Uri(this.animation.getHonkAudioPath());
				this.soundMediaElement.Play();
				Thread thread = new Thread(() => handleAnimalLeavingTransition(duration));
				thread.Start();
			}
			else
			{
				// check if honk_playing
				//		set up new timer for duration of the audio
				//		set honk_playing to true
				//		continue to do nothing until duration of audio is over
				//		when duration is over, reset honk_playing value to false
				Thread thread = new Thread(playSound_Tick);
				thread.Start();
				
			}
        }

        public void windshieldWiperPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                double duration = this.animation.Raining(this.GUI);
                sceneStatus = SceneStatus.Raining;
                Thread thread = new Thread(() => handleRainingTransition(duration));
                thread.Start();
            }
        }

        public void animalButtonPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.rollAnimal();
                double duration = this.animation.animalAppears(this.GUI);
                sceneStatus = SceneStatus.AnimalShowing;
                Thread thread = new Thread(() => handleAnimalShowingTransition(duration));
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

        public void setAudio()
        {
            switch (sceneStatus)
            {
                case SceneStatus.Moving:
                case SceneStatus.AnimalLeaving:
                    break;
                default:
                    if (this.soundMediaElement.CanPause)
                    {
                        this.soundMediaElement.Pause();
                    }
                    break;
            }
        }

        public void switchEnvironment()
        {
            this.animation = (Animation)this.animations[(this.animations.IndexOf(this.animation) + 1) % this.animations.Count];
            this.animation.carStops(this.GUI);
			this.steerTime = TimeSpan.Zero;
        }

        public void handleAnimalShowingTransition(double duration)
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.TotalSeconds > duration)
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

        public void handleAnimalLeavingTransition(double duration)
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.TotalSeconds > duration)
                {
                    if (sceneStatus == SceneStatus.AnimalLeaving)
                    {
                        this.Dispatcher.Invoke((Action)delegate()
                        {
                            this.animation.carStops(this.GUI);
                            if (this.soundMediaElement.CanPause)
                            {
                                this.soundMediaElement.Pause();
                            }
                        });
                        sceneStatus = SceneStatus.Still;
                    }
                    break;
                }
            }
        }


        public void handleRainingTransition(double duration)
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                TimeSpan d = DateTime.Now - startTime;
                if (d.TotalSeconds > duration && sceneStatus == SceneStatus.Raining)
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

		private void soundMediaElement_MediaOpened(object sender, RoutedEventArgs e)
		{
			this.soundMediaElement.Source = new Uri(this.animation.getHonkAudioPath());
			this.soundMediaElement.Play();
		}

		private void playSound_Tick()
		{
			this.Dispatcher.Invoke((Action)delegate()
			{
				this.honkAudio.Play();
			});
		}
    }
}
