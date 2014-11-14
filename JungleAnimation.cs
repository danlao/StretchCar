
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class JungleAnimation : Animation
    {
        public JungleAnimation()
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

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\crocodile_entering.html", rootPath + "\\crocodile_still.html", rootPath + "\\crocodile_leaving.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\giraffe_entering.html", rootPath + "\\giraffe_still.html", rootPath + "\\giraffe_leaving.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\lion_entering.html", rootPath + "\\lion_still.html", rootPath + "\\lion_leaving.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\monkey_entering.html", rootPath + "\\monkey_still.html", rootPath + "\\monkey_leaving.html"));

            this.animalShowingScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[0]).Item1;
            this.animalStillScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[0]).Item2;
            this.animalLeavingScenePath = ((Tuple<String, String, String>)this.animalScenePathTuples[0]).Item3;
        }

    }
}
