using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace Zaptos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        private const int MINIMUM_SPLASH_TIME = 1500; // Miliseconds 
        private const int SPLASH_FADE_TIME = 500;     // Miliseconds 
 
        protected override void OnStartup(StartupEventArgs e) 
        { 
            // Step 1 - Load the splash screen 
            SplashScreen splash = new SplashScreen("Zaptos.png"); 
            splash.Show(true,false); 
 
            // Step 2 - Start a stop watch 
            Stopwatch timer = new Stopwatch(); 
            timer.Start(); 
 
            // Step 3 - Load your windows but don't show it yet 
            //base.OnStartup(e); 
           // MainWindow main = new MainWindow(); 
 
            // Step 4 - Make sure that the splash screen lasts at least two seconds 
            timer.Stop(); 
            int remainingTimeToShowSplash = MINIMUM_SPLASH_TIME - (int)timer.ElapsedMilliseconds; 
            if (remainingTimeToShowSplash > 0) 
                Thread.Sleep(remainingTimeToShowSplash); 
 
            // Step 5 - show the page 
            splash.Close(TimeSpan.FromMilliseconds(SPLASH_FADE_TIME)); 
           // main.Show(); 
        } 
    }
    }

