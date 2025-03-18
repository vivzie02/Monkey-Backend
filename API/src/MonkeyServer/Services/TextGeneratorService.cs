using MonkeyServer.DTOs;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// TextGeneratorService
    /// </summary>
    public class TextGeneratorService : ITextGeneratorService
    {
        private readonly IBookService _bookService;
        private readonly IGrammarCheckService _grammarCheckService;

        private const int GENERATION_DELAY = 10;
        private const int CANCELLATION_CHANCE = 17;
        private readonly Random _random = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="grammarCheckService"></param>
        public TextGeneratorService(IBookService bookService, IGrammarCheckService grammarCheckService)
        {
            _bookService = bookService;
            _grammarCheckService = grammarCheckService;
        }

        /// <summary>
        /// generate random Text
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task GenerateText(CancellationToken cancellationToken)
        {
            var bookBuilder = new StringBuilder();
            var wordLength = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var word = RandomWord();

                if (DictionaryService.IsWordInDictionary(word))
                {
                    bookBuilder.Append(word);
                    bookBuilder.Append(" ");
                    wordLength++;
                }
                else if (wordLength > 0)
                {
                    if (wordLength >= 3 && (await _grammarCheckService.CheckGrammar(bookBuilder.ToString())))
                    {
                        var book = new BookDTO()
                        {
                            Content = bookBuilder.ToString(),
                            NumberOfWords = wordLength
                        };
                        await _bookService.SaveBookAsync(book);
                    }
                    bookBuilder = new StringBuilder();
                    wordLength = 0;
                }

                await Task.Delay(GENERATION_DELAY, cancellationToken).ConfigureAwait(false);
            }
        }

        private string RandomWord()
        {
            var wordBuilder = new StringBuilder();

            do
            {
                wordBuilder.Append(RandomLetter());
            } while (CANCELLATION_CHANCE < GetRandomNumber(0, 101));

            return wordBuilder.ToString();
        }

        private char RandomLetter()
        {
            const string options = "ABCDEFGHIJKLMNOPQRSTUVXYZÄÖÜabcdefghijklmnopqrstuvwxyzäöüß";
            var optionsLength = options.Length;
            return options[GetRandomNumber(0, optionsLength)];
        }

        private int GetRandomNumber(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
