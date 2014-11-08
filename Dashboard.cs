using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class Dashboard
    {
        private ArrayList items;
        private bool activeStatus;

        public Dashboard()
        {
            this.items = new ArrayList();
            this.activeStatus = false;
        }

        public void activate() {
            this.activeStatus = true;
        }

        public void deactivate()
        {
            this.activeStatus = false;
        }

        public bool isActive()
        {
            return this.activeStatus;
        }

        public void addItem(DashboardItem item)
        {
            this.items.Add(item);
        }

        public DashboardItem findItem(int x, int y)
        {
            foreach (DashboardItem item in items) {
                if (item.findPoint(x, y))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
