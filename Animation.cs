using System;
using System.Collections;
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
		protected String audioPath;
		protected String animalStillScenePath;
		protected Tuple<String, double> animalShowingScenePath;
		protected Tuple<String, double> animalLeavingScenePath;
		protected Tuple<String, double> rainingScenePath;

        protected ArrayList animalScenePathTuples;
		protected ArrayList animalSceneDuration;

        protected int numAnimal;

        protected Random rand;

        public Animation()
        {
            this.animalScenePathTuples = new ArrayList();
			this.animalSceneDuration = new ArrayList();

            this.rand = new Random();
        }

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

        public String getAudioPath()
        {
            return this.audioPath;
        }

        public void carMoves(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.movingScenePath);
        }

        public void carStops(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.stillScenePath);
        }

        public double animalAppears(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalShowingScenePath.Item1);
			return this.animalShowingScenePath.Item2;
        }

        public void animalStill(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalStillScenePath);
        }

        public double animalLeaves(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalLeavingScenePath.Item1);
			return this.animalLeavingScenePath.Item2;
        }

        public double Raining(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.rainingScenePath.Item1);
			return this.rainingScenePath.Item2;
        }

        public void rollAnimal()
        {
            int randInt = this.rand.Next(0, this.numAnimal);
            this.animalShowingScenePath = new Tuple<string,double>(((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item1, 
					((Tuple<double, double>)this.animalSceneDuration[randInt]).Item1);
            this.animalStillScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item2;
			this.animalLeavingScenePath = new Tuple<string, double>(((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item3, 
					((Tuple<double, double>)this.animalSceneDuration[randInt]).Item1);
        }
    }
}
