using System;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class AnimalButton : DashboardItem
    {
		private int radius;

        public AnimalButton(int x, int y, int radius)
            : base(x, y)
        {
            this.radius = radius;
        }

        public override bool findPoint(int x, int y)
        {
            double distance = Math.Sqrt(Math.Pow(x - this.xCoord, 2) + Math.Pow(y - this.yCoord, 2));

			if (distance <= (double)this.radius)
			{
				return true;
			}

			return false;
        }

    }
}
