using System;
using System.Diagnostics;

namespace My.CoachManager.CrossCutting.Logging
{
    internal class TraceGroup : IDisposable
    {
        #region Fields

        private static readonly ILogger Logger = LogManager.Logger;
        private readonly string _title;
        private readonly Stopwatch _stopwatch;

        #endregion

            internal TraceGroup(string title)
            {
                string str = title;
                _title = str ?? throw new ArgumentNullException(nameof(title));

                Logger.Trace("START - " + _title);

                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
            
            public void Dispose()
            {
                _stopwatch.Stop();
                Logger.Trace($"END - {_title} : {_stopwatch.Elapsed}");
            }
    }
    }