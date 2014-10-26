using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace Microsoft.Samples.Kinect.DepthBasics
{

    

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        private string rootpath = System.AppDomain.CurrentDomain.BaseDirectory;

        public GameWindow()
        {
            InitializeComponent();

            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));

            Console.WriteLine(System.IO.Path.Combine(rootpath, "html\\basic.html"));
            this.GUI.Source = new Uri(System.IO.Path.Combine(rootpath, "html\\basic.html"));


        }

        public void showBasic()
        {
            this.GUI.Source = new Uri(System.IO.Path.Combine(rootpath, "html\\basic.html"));
        }

        public void showMonkey()
        {
            this.GUI.Source = new Uri(System.IO.Path.Combine(rootpath, "html\\monkey.html"));
        }

        public void showCrocodile()
        {
            this.GUI.Source = new Uri(System.IO.Path.Combine(rootpath, "html\\crocodile.html"));
        }
    }
}
