using System.Collections.Generic;
using System.Threading.Tasks;
using ChatBot.Models;
using Npgsql;

namespace ChatBot
{
    public static class DataBase
    {
        
        /// <summary>
        /// Return all words from database.
        /// </summary>
        /// <returns>List of words.</returns>
        public static async Task<List<Word>> GetAllWords()
        {
            List<Word> result = new List<Word>();

            var conn = new NpgsqlConnection(PrivateSettings.ConnectionString);
            await conn.OpenAsync();

            string script = "SELECT * FROM words;";
            var command = new NpgsqlCommand(script, conn);
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(new Word() {Id = reader.GetInt32(0), Text = reader.GetString(1)});
            }
            
            await conn.CloseAsync();
            return result;
        }

        
        /// <summary>
        /// Add new word into database.
        /// </summary>
        /// <param name="word">Word for adding into DB.</param>
        /// <returns>Word with unique id from database.</returns>
        public static async Task<Word> AddNewWord(Word word)
        {
            var conn = new NpgsqlConnection(PrivateSettings.ConnectionString);
            await conn.OpenAsync();

            string script = $"INSERT INTO words VALUE text='{word.Text}';";
            var command = new NpgsqlCommand(script, conn);
            await command.ExecuteNonQueryAsync();

            script = $"SELECT * FROM words WHERE text='{word.Text}';";
            command = new NpgsqlCommand(script, conn);
            var reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                word.Id = reader.GetInt32(0);
            }
            
            await conn.CloseAsync();
            return word;
        }

        
        /// <summary>
        /// Delete word from database.
        /// </summary>
        /// <param name="word">Word for deleting from database.</param>
        public static async Task DeleteWord(Word word)
        {
            var conn = new NpgsqlConnection(PrivateSettings.ConnectionString);
            await conn.OpenAsync();
            
            string script = $"DELETE FROM words * WHERE id={word.Id};";
            var command = new NpgsqlCommand(script, conn);
            await command.ExecuteNonQueryAsync();
            
            await conn.CloseAsync();
        }
    }
}