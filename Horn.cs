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
			// formula from http://totologic.blogspot.com/2014/01/accurate-point-in-triangle-test.html

			double denom = (this.secondY - this.thirdY) * (this.xCoord - this.thirdX) + 
				(this.thirdX - this.secondX) * (this.yCoord - this.thirdY);

			double a_numer = (this.secondY - this.thirdY) * ((double)x - this.thirdX) + 
				(this.thirdX - this.secondX) * ((double)y - this.thirdY);

			double b_numer = (this.thirdY - this.yCoord) * ((double)x - this.thirdX) + 
				(this.xCoord - this.thirdX) * ((double)y - this.thirdY);

			double a = a_numer / denom;
			double b = b_numer / denom;
			double c = 1 - a - b;

			return	0 <= a && a <= 1 &&
					0 <= b && b <= 1 &&
					0 <= c && c <= 1;

        }
    }
}
