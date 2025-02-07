using IO.Swagger.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    /// <summary>
    /// ITextGeneratorService
    /// </summary>
    public interface IMonkeyManagerService
    {
        /// <summary>
        /// Start a new task
        /// </summary>
        /// <param name="taskFunction"></param>
        /// <returns></returns>
        MonkeyOutputDTO StartTask(Func<CancellationToken, Task> taskFunction);
    }
}
