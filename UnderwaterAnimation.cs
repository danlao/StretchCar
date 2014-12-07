
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class UnderwaterAnimation : Animation
    {
        public UnderwaterAnimation()
            : base()
        {
            this.generatePath();

            String[] lines = System.IO.File.ReadAllLines(rootPath + "\\time_properties.txt");
            foreach (string line in lines)
            {
                String[] values = line.Split(' ');

                if (values[0] == "driving")
                {
                    continue;
                }

                if (values[0] == "raining")
                {
                    //this.rainingScenePath = new Tuple<string, double>(rootPath + "\\raining.html", Convert.ToDouble(values[1])); //there isn't a raining scene'
                    continue;
                }

                // Tuple(start duration, end duration)
                this.animalSceneDuration.Add(new Tuple<double, double>(Convert.ToDouble(values[1]), Convert.ToDouble(values[2])));
            }
            this.rollAnimal();
        }

        private void generatePath()
        {
            base.generateRootPath();
            rootPath += "\\html\\Underwater";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html";
            this.drivingAudioPath = rootPath + "\\Underwater_Song.mp3";
            this.honkAudioPath = rootPath + "\\Horn.mp3";

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\crab_enter.html", rootPath + "\\crab_still.html", rootPath + "\\crab_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\dolphin_enter.html", rootPath + "\\dolphin_still.html", rootPath + "\\dolphin_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\fish_enter.html", rootPath + "\\fish_still.html", rootPath + "\\fish_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\whale_enter.html", rootPath + "\\whale_still.html", rootPath + "\\whale_leave.html"));

            this.numAnimal = 4;
        }

    }
}
