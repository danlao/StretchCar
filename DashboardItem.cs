using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    abstract class DashboardItem
    {
        protected int xCoord;
        protected int yCoord;

        public DashboardItem(int x, int y)
        {
            this.xCoord = x;
            this.yCoord = y;
        }

        public abstract bool findPoint(int x, int y);

    }
}
