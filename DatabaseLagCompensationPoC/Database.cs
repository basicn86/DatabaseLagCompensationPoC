using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DatabaseLagCompensationPoC
{
    public class Database
    {
        private const int FakeLag = 10000; // in ms

        private SqliteConnection connection;

        public Database()
        {
            connection = new SqliteConnection("Data Source=database.db");
            connection.Open();
            CreateTable();
        }

        //close connection on destruction
        ~Database()
        {
            connection.Close();
        }

        //create table if it does not exist
        private void CreateTable()
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Messages (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Content TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        public void ResetTable()
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                DROP TABLE IF EXISTS Messages;
            ";
            command.ExecuteNonQuery();
            CreateTable();

            command = connection.CreateCommand();

            //insert some test data
            command.CommandText = @"
                INSERT INTO Messages (Name, Content) VALUES
                    ('John', 'Hello World!'),
                    ('Jane', 'Hi there!'),
                    ('John', 'How are you?'),
                    ('Jane', 'I am fine, thanks!'),
                    ('John', 'Goodbye!');";

            command.ExecuteNonQuery();
        }

        public async Task<List<Msg>> GetMessages()
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Id, Name, Content FROM Messages;
            ";
            var reader = command.ExecuteReader();

            var messages = new List<Msg>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var content = reader.GetString(2);
                messages.Add(new Msg(id, name, content));
            }

            //fake lag
            await Task.Delay(FakeLag);

            return messages;
        }

        public async Task UpdateMessage(int id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Messages SET Content = 'The contents have been updated' WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();

            await Task.Delay(FakeLag);
        }
    }
}
