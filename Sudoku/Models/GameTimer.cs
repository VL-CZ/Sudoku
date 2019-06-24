using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sudoku.Models
{
    internal class GameTimer : ObservableObject
    {
        /// <summary>
        /// instance of timer
        /// </summary>
        private DispatcherTimer timer;

        private int minutes = 0;
        
        /// <summary>
        /// elapsed minutes
        /// </summary>
        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
                RaisePropertyChanged();
            }
        }

        private int seconds = 0;

        /// <summary>
        /// elapsed seconds since last completed minute
        /// </summary>
        public int Seconds
        {
            get
            {
                return seconds % 60;
            }
            set
            {
                seconds = value;
                if (seconds % 60 == 0)
                {
                    Minutes++;
                }
                RaisePropertyChanged();
            }
        }

        public GameTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        /// <summary>
        /// method that is invoked on timer_tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Seconds++;
        }

        /// <summary>
        /// stop measuring time
        /// </summary>
        public void StopTimer()
        {
            timer.Stop();
            timer.Tick -= DispatcherTimer_Tick;
        }
    }
}