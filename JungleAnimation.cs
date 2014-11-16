
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class JungleAnimation : Animation
    {
        public JungleAnimation()
            : base()
        {
            this.generatePath();
        }

        private void generatePath()
        {
            base.generateRootPath();
            rootPath += "\\html\\Jungle";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html";
            this.audioPath = rootPath + "\\Jungle_Song.mp3";

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\crocodile_enter.html", rootPath + "\\crocodile_still.html", rootPath + "\\crocodile_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\giraffe_enter.html", rootPath + "\\giraffe_still.html", rootPath + "\\giraffe_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\lion_enter.html", rootPath + "\\lion_still.html", rootPath + "\\lion_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\monkey_enter.html", rootPath + "\\monkey_still.html", rootPath + "\\monkey_leave.html"));

            this.numAnimal = 4;
            this.rollAnimal();

            this.rainStartScenePath = rootPath + "\\rain_start.html";
            this.rainingScenePath = rootPath + "\\raining.html";
            this.rainEndScenePath = rootPath + "\\rain_end.html";
        }

    }
}
