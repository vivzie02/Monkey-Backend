using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using IO.Swagger.DTOs;
using Microsoft.Extensions.Hosting;

namespace IO.Swagger.Services
{
    /// <summary>
    /// TextGeneratorService
    /// </summary>
    public class TextGeneratorService : ITextGeneratorService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private readonly IBookService _bookService;

        private const int GENERATION_DELAY = 10;
        private const int CANCELLATION_CHANCE = 17;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookService"></param>
        public TextGeneratorService(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// generate random Text
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task GenerateText(CancellationToken cancellationToken)
        {
            var bookBuilder = new StringBuilder();
            var wordLength = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var word = RandomWord();

                if (DictionaryService.IsWordInDictionary(word))
                {
                    Debug.WriteLine($"Found word in dictionary: {word}");
                    bookBuilder.Append(word);
                    bookBuilder.Append(" ");
                    wordLength++;
                }
                else if(wordLength > 0)
                {
                    if(wordLength >= 2)
                    {
                        var book = new BookDTO()
                        {
                            Content = bookBuilder.ToString(),
                            NumberOfWords = wordLength
                        };
                        Debug.WriteLine($"Saving book: {book}");
                        log.Info("Saving book...");
                        log.Info(book);
                        await _bookService.SaveBook(book);
                    }
                    bookBuilder = new StringBuilder();
                    wordLength = 0;
                }

                await Task.Delay(GENERATION_DELAY, cancellationToken).ConfigureAwait(false);
            }
        }

        private static string RandomWord()
        {
            var wordBuilder = new StringBuilder();

            do
            {
                wordBuilder.Append(RandomLetter());
            } while (CANCELLATION_CHANCE < GetSecureRandomNumber(1, 100));

            return wordBuilder.ToString();
        }

        private static char RandomLetter()
        {
            const string options = "ABCDEFGHIJKLMNOPQRSTUVXYZÄÖÜabcdefghijklmnopqrstuvwxyzäöüß";
            var optionsLength = options.Length;
            return options[GetSecureRandomNumber(0, optionsLength - 1)];
        }

        private static int GetSecureRandomNumber(int minValue, int maxValue)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                int randomInt = BitConverter.ToInt32(randomBytes, 0) & int.MaxValue; // Convert to positive number
                return minValue + (randomInt % (maxValue - minValue + 1));
            }
        }
    }
}
