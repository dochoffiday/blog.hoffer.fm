using System;
using System.Diagnostics;

namespace AJ.UtiliTools
{
    public class UtiliTime : IDisposable
    {
        private Stopwatch _watch;
        private int _milliseconds;

        public UtiliTime(int milliseconds)
        {
            _watch = new System.Diagnostics.Stopwatch();
            _watch.Start();
            _milliseconds = milliseconds;
        }

        public void End()
        {
            int remainingMilliseconds = Convert.ToInt32(_milliseconds - _watch.ElapsedMilliseconds);

            _watch.Stop();

            if (remainingMilliseconds > 0)
                System.Threading.Thread.Sleep(remainingMilliseconds);
        }

        void IDisposable.Dispose()
        {
            this.End();
        }
    }
}