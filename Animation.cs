using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    abstract class Animation
    {
        protected String movingScene;
        protected String stillScene;

        public virtual void carMoves();
        public virtual void carStops();
        public virtual void animalAppears();
        public virtual void animalLeaves();

    }
}
