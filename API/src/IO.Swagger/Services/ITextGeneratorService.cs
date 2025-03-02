using System.Threading;
using System.Threading.Tasks;

namespace IO.Swagger.Services
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
