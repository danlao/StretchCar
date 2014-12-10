
namespace Microsoft.Samples.Kinect.DepthBasics
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            StretchCar stretchCar = new StretchCar();
        }
    }
}
