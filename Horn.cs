using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class Horn : DashboardItem
    {
        private int secondX;
        private int secondY;
        private int thirdX;
        private int thirdY;

        public Horn(int x, int y, int secondX, int secondY, int thirdX, int thirdY)
            : base(x, y)
        {
            this.secondX = secondX;
            this.secondY = secondY;
            this.thirdX = thirdX;
            this.thirdY = thirdY;
        }

        public override bool findPoint(int x, int y)
        {
            return true;
        }
    }
}
