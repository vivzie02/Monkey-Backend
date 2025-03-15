using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// IGrammarCheckService
    /// </summary>
    public interface IGrammarCheckService
    {
        /// <summary>
        /// check if grammar is ok
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        Task<bool> CheckGrammar(string sentence);
    }
}
