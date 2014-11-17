using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class CountrysideAnimation : Animation
    {
        public CountrysideAnimation()
            : base()
        {
            this.generatePath();
        }

        private void generatePath()
        {
            base.generateRootPath();
            rootPath += "\\html\\Countryside";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html"; 
            //this.audioPath = rootPath + "\\Countryside_Song.mp3"; //no audio

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\bird_enter.html", rootPath + "\\bird_still.html", rootPath + "\\bird_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\cow_enter.html", rootPath + "\\cow_still.html", rootPath + "\\cow_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\dog_enter.html", rootPath + "\\dog_still.html", rootPath + "\\dog_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\rabbit_enter.html", rootPath + "\\rabbit_still.html", rootPath + "\\rabbit_leave.html"));

            this.numAnimal = 4;
            this.rollAnimal();

            //this.rainStartScenePath = rootPath + "\\rain_start.html";  no rain files
            //this.rainingScenePath = rootPath + "\\raining.html";
            //this.rainEndScenePath = rootPath + "\\rain_end.html";
        }

    }
}