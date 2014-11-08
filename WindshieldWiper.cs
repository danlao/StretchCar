using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class WindshieldWiper : DashboardItem
    {
        private int secondX;
        private int secondY;

        public WindshieldWiper(int x, int y, int secondX, int secondY)
            : base(x, y)
        {
            this.secondX = secondX;
            this.secondY = secondY;
        }

        public override bool findPoint(int x, int y)
        {
            int left = this.secondX > this.xCoord ? this.xCoord : this.secondX;
            int right = this.secondX < this.xCoord ? this.xCoord : this.secondX;
            if (x < left || x > right)
            {
                return false;
            }
            int top = this.secondY > this.yCoord ? this.yCoord : this.secondY;
            int bottom = this.secondY < this.yCoord ? this.yCoord : this.secondY;
            if (y < top || y > bottom)
            {
                return false;
            }
            return true;
        }
    }
}
