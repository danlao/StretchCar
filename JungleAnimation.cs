
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
            rootPath += "\\html\\Jungle";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html";
            this.drivingAudioPath = rootPath + "\\Jungle_Song.mp3";
            this.honkAudioPath = rootPath + "\\Horn.mp3";

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\crocodile_enter.html", rootPath + "\\crocodile_still.html", rootPath + "\\crocodile_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\giraffe_enter.html", rootPath + "\\giraffe_still.html", rootPath + "\\giraffe_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\lion_enter.html", rootPath + "\\lion_still.html", rootPath + "\\lion_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\monkey_enter.html", rootPath + "\\monkey_still.html", rootPath + "\\monkey_leave.html"));

            this.numAnimal = 4;
        }

    }
}
