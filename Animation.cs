﻿using System;
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
        protected String animalShowingScenePath;
        protected String animalStillScenePath;
        protected String animalLeavingScenePath;
        protected String rainStartScenePath;
        protected String rainingScenePath;
        protected String rainEndScenePath;
        protected String audioPath;

        protected ArrayList animalScenePathTuples;

        protected int numAnimal;

        protected Random rand;

        public Animation()
        {
            this.animalScenePathTuples = new ArrayList();
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

        public void animalAppears(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalShowingScenePath);
        }

        public void animalStill(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalStillScenePath);
        }

        public void animalLeaves(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.animalLeavingScenePath);
        }

        public void Raining(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.rainingScenePath);
        }

        public void rollAnimal()
        {
            int randInt = this.rand.Next(0, this.numAnimal);
            this.animalShowingScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item1;
            this.animalStillScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item2;
            this.animalLeavingScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[randInt]).Item3;
        }
    }
}
