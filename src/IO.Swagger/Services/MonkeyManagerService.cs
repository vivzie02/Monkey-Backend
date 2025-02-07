using IO.Swagger.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    /// <summary>
    /// MonkeyManagerService
    /// </summary>
    public class MonkeyManagerService : IMonkeyManagerService
    {
        private readonly Dictionary<Guid, CancellationTokenSource> _tasks = new Dictionary<Guid, CancellationTokenSource>();

        /// <summary>
        /// Starts a new Monkey Task
        /// </summary>
        /// <param name="taskFunction"></param>
        /// <returns></returns>
        public MonkeyOutputDTO StartTask(Func<CancellationToken, Task> taskFunction)
        {
            var jobId = Guid.NewGuid();
            var cts = new CancellationTokenSource();

            _tasks.Add(jobId, cts);
            Task.Run(() => taskFunction(cts.Token));
            
            return new MonkeyOutputDTO
            {
                MonkeyId = jobId
            };
        }

        /// <summary>
        /// StopTask
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public bool StopTask(Guid jobId)
        {
            if(_tasks.TryGetValue(jobId, out var cts))
            {
                cts.Cancel();
                _tasks.Remove(jobId);
                return true;
            }
            return false;
        }
    }
}
