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
using MonkeyServer.DTOs;
using Microsoft.Extensions.Hosting;
using MonkeyServer;

namespace MonkeyServer.Services
{
    /// <summary>
    /// TextGeneratorService
    /// </summary>
    public class TextGeneratorService : ITextGeneratorService
    {
        private readonly IBookService _bookService;

        private const int GENERATION_DELAY = 10;
        private const int CANCELLATION_CHANCE = 17;
        private readonly Random _random = new Random();

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
                    if (wordLength >= 3)
                    {
                        var book = new BookDTO()
                        {
                            Content = bookBuilder.ToString(),
                            NumberOfWords = wordLength
                        };
                        await _bookService.SaveBook(book);
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
