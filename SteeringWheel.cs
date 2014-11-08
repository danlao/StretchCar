using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class SteeringWheel : DashboardItem
    {
        private int radius;

        public SteeringWheel(int x, int y, int radius) : base(x, y)
        {
            this.radius = radius;
        }

        public override bool findPoint(int x, int y)
        {            
            return false;
        }

    }
}
