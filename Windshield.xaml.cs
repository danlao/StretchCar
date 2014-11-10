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
            AnimalShowing,
            AnimalLeaving
        };

        SceneStatus sceneStatus;
        Animation animation;
        ArrayList animations;

        private string rootpath = System.AppDomain.CurrentDomain.BaseDirectory;

        public Windshield()
        {
            InitializeComponent();

            this.sceneStatus = SceneStatus.Still;
            this.animation = new JungleAnimation();
            this.animation.carStops(this.GUI);
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
            if (sceneStatus == SceneStatus.AnimalShowing)
            {
                this.animation.animalLeaves(this.GUI);
                sceneStatus = SceneStatus.AnimalLeaving;
            }
        }

        public void windshieldWiperPressed()
        {
			// TODO: implement this
        }

        public void animalButtonPressed()
        {
            if (sceneStatus == SceneStatus.Moving || sceneStatus == SceneStatus.Still)
            {
                this.animation.animalAppears(this.GUI);
                sceneStatus = SceneStatus.AnimalShowing;
            }
        }

        public void switchEnvironment()
        {
            this.animation = (Animation)this.animations[(this.animations.IndexOf(this.animation) + 1) % this.animations.Count];
        }
    }
}
