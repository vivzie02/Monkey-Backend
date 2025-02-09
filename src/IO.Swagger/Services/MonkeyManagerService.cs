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
    public static class MonkeyManagerService
    {
        private static readonly Dictionary<Guid, CancellationTokenSource> _tasks = new Dictionary<Guid, CancellationTokenSource>();

        /// <summary>
        /// Starts a new Monkey Task
        /// </summary>
        /// <param name="taskFunction"></param>
        /// <returns></returns>
        public static MonkeyOutputDTO StartTask(Func<CancellationToken, Task> taskFunction)
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
        /// <param name="monkeyId"></param>
        /// <returns></returns>
        public static MessageDTO StopTask(Guid monkeyId)
        {
            if(_tasks.TryGetValue(monkeyId, out var cts))
            {
                cts.Cancel();
                _tasks.Remove(monkeyId);
                return new MessageDTO
                {
                    Message = "Successfully stopped task"
                };
            }
            return new MessageDTO
            {
                Message = "Task not found"
            };
        }

        /// <summary>
        /// StopAll
        /// </summary>
        /// <returns></returns>
        public static MessageDTO StopAll()
        {
            foreach(var monkeyId in _tasks.Keys)
            {
                _tasks[monkeyId].Cancel();
            }
            _tasks.Clear();

            return new MessageDTO
            {
                Message = "Successfully stopped task"
            };
        }
    }
}
