using System.Threading;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// ITextGeneratorService
    /// </summary>
    public interface ITextGeneratorService
    {
        /// <summary>
        /// GenerateText
        /// </summary>
        Task GenerateText(CancellationToken cancellationToken);
    }
}
