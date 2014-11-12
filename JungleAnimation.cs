﻿
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
        }

		public override String getAudioPath()
		{
			return this.audioPath;
		}

        public override void carMoves(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.movingScenePath);
        }

        public override void carStops(System.Windows.Controls.WebBrowser webBrowser)
        {
            webBrowser.Source = new Uri(this.stillScenePath);
        }

        public override void animalAppears(System.Windows.Controls.WebBrowser webBrowser)
        {
			// TODO: implement when receive animation files
        }

        public override void animalLeaves(System.Windows.Controls.WebBrowser webBrowser)
        {
			// TODO: implement when receive animation files
        }

    }
}
