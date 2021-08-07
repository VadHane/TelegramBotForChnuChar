using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Models
{
    public struct Word
    {
        /// <summary>
        /// Unique id of this word from database.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Text of this word.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Default constructor for initial all members.
        /// </summary>
        /// <param name="text">Text of this word.</param>
        private Word(string text)
        {
            Id = 0;
            Text = text;
        }

        /// <summary>
        /// Implicit operator for cast string to Word
        /// </summary>
        /// <param name="text">Text of this word.</param>
        public static implicit operator Word(string text)
        {
            return new Word(text);
        }
    }
    
    public class Dictionary
    {
        // List of all word from this dictionary
        private static List<Word> Words = DataBase.GetAllWords().Result;

        
        /// <summary>
        /// Delete word from dictionary.
        /// </summary>
        /// <param name="word">Word for deleting.</param>
        public static async Task Delete(Word word)
        {
            word = Words.FirstOrDefault(word_ => word_.Text == word.Text);

            if (word.Text == null)
            {
                return;
            }
            
            await DataBase.DeleteWord(word);
            Words.RemoveAll(word_ => word_.Id == word.Id);

            Console.WriteLine($"[database] Deleted word. [word - {word.Text}];");
            await Program.bot.SafeSendMessageAsync(Settings.LogChat, Models.Text.DeleteWord(word), ParseMode.Html);
        }

        
        /// <summary>
        /// Add new word into dictionary.
        /// </summary>
        /// <param name="word">Word for adding.</param>
        public static async Task Add(Word word)
        {
            word.Text = word.Text.ToLower();
            
            if (Words.Exists(word_ => word_.Text == word.Text))
            {
                await Program.bot.SafeSendMessageAsync(Settings.LogChat, "Таке слово уже є в словнику!");
                return;
            }

            word = await DataBase.AddNewWord(word);
            Words.Add(word);

            Console.WriteLine($"[database] Aded new word. [word - {word.Text}];");
            await Program.bot.SafeSendMessageAsync(Settings.LogChat, Text.AddWord(word), ParseMode.Html);
        }

        
        /// <summary>
        /// Search word in dictionary.
        /// </summary>
        /// <param name="word">Word for searching.</param>
        /// <returns>True - if word was found and false if was not be.</returns>
        public static bool Search(Word word)
        {
            return Words.Exists(word_ => word_.Text == word.Text);
        }
    }
}