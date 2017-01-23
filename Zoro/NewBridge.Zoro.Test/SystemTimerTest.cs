using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Timers;
using System.Diagnostics;

namespace NewBridge.Zoro.Test
{
    [TestClass]
    public class SystemTimerTest
    {
        private static System.Timers.Timer aTimer;
        private static int i;

        [TestMethod]
        public void TimerTest()
        {
            SetTimer();
            
            Trace.WriteLine("The application started at {0}", DateTime.Now.ToLongTimeString());
            while (i < 10)
            {
                Trace.WriteLine(i);
            }
            aTimer.Stop();
            aTimer.Dispose();

            Trace.WriteLine("Terminating the application...");
            Assert.IsTrue(i > 0);
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            i = 0;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Trace.WriteLine("The Elapsed event was raised at {0}",e.SignalTime.ToLongTimeString());
            i++;
        }
    }
}

