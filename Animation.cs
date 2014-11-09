using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    abstract class Animation
    {
        protected String rootPath;
        protected String movingScenePath;
        protected String stillScenePath;

        public void generateRootPath()
        {
            rootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            while (true)
            {
                if (rootPath.Substring(rootPath.LastIndexOf("\\") + 1).Equals("StretchCar"))
                {
                    break;
                }
                rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            }
        }

        public abstract void carMoves(System.Windows.Controls.WebBrowser webBrowser);
        public abstract void carStops(System.Windows.Controls.WebBrowser webBrowser);
        public abstract void animalAppears(System.Windows.Controls.WebBrowser webBrowser);
        public abstract void animalLeaves(System.Windows.Controls.WebBrowser webBrowser);
    }
}
