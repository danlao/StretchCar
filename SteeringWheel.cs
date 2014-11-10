using System;

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
            // 1. find the distance from x,y to xCoord,yCoord
			// 2. Compare this distance to radius
			//		if less than, return true

			double distance = Math.Sqrt(Math.Pow(x - this.xCoord, 2) + Math.Pow(y - this.yCoord, 2));

			if (distance <= (double)radius)
			{
				return true;
			}

			return false;
        }

    }
}
