using System;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class CountrysideAnimation : Animation
    {
        public CountrysideAnimation()
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
            rootPath += "\\html\\Countryside";
            this.stillScenePath = rootPath + "\\still.html";
            this.movingScenePath = rootPath + "\\driving.html";
            this.drivingAudioPath = rootPath + "\\Countryside_Song.mp3"; //use jungle sone for now
            this.honkAudioPath = rootPath + "\\Horn.mp3";

            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\rabbit_enter.html", rootPath + "\\rabbit_still.html", rootPath + "\\rabbit_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\cow_enter.html", rootPath + "\\cow_still.html", rootPath + "\\cow_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\dog_enter.html", rootPath + "\\dog_still.html", rootPath + "\\dog_leave.html"));
            this.animalScenePathTuples.Add(new Tuple<String, String, String>(rootPath + "\\bird_enter.html", rootPath + "\\bird_still.html", rootPath + "\\bird_leave.html"));

            this.numAnimal = 4;
        }

    }
}