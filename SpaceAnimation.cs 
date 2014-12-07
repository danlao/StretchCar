
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class SpaceAnimation : Animation
    {
        public SpaceAnimation()
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
                    this.rainingScenePath = new Tuple<string, double>(rootPath + "\\raining.html", Convert.ToDouble(values[1]));
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
            rootPath += "\\html\\Space";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html";
            this.drivingAudioPath = rootPath + "\\Space_Song.mp3";
            this.honkAudioPath = rootPath + "\\Horn.mp3";

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\alien_enter.html", rootPath + "\\alien_still.html", rootPath + "\\alien_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\meteor_enter.html", rootPath + "\\meteor_still.html", rootPath + "\\meteor_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\planets_enter.html", rootPath + "\\planets_still.html", rootPath + "\\planets_leave.html"));

            this.numAnimal = 3;
        }

    }
}
