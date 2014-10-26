﻿//------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

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
            GameWindow gameWindow = new GameWindow();

            MainWindow mainWindow = new MainWindow();
            mainWindow.GameWindow = gameWindow;

            mainWindow.Show();
            gameWindow.Show();

        }
    }
}