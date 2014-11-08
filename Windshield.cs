using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class Windshield
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

        public Windshield()
        {
            this.sceneStatus = SceneStatus.Still;
            
        }

        public void steerPressed()
        {
            if (sceneStatus == SceneStatus.Still)
            {
                this.animation.carMoves();
            }
        }

        public void hornPressed()
        {
            if (sceneStatus == SceneStatus.AnimalShowing)
            {
                this.animation.animalLeaves();
            }
        }

        public void windshieldWiperPressed()
        {

        }

        public void animalButtonPressed()
        {
            if (sceneStatus == SceneStatus.Moving || sceneStatus == SceneStatus.Still)
            {
                this.animation.animalAppears();
            }
        }

        public void switchEnvironment()
        {
            this.animation = (Animation) this.animations[this.animations.IndexOf(this.animation)];
        }
    }
}
