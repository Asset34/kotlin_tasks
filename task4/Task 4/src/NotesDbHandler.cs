using Npgsql;

namespace Task_4
{
    public static class NotesDbHandler
    {
        private static readonly string _connectionString = "Host=localhost;Username=postgres;Password=password;Database=Notes";
        private static readonly string _dbNotesTableName = "notes";

        public static async Task<List<Note>?> ReceiveNoteListAsync()
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var command = dataSource.CreateCommand($"SELECT * FROM {_dbNotesTableName}");
            await using var reader = await command.ExecuteReaderAsync();

            // Validate
            if (!reader.HasRows)
            {
                return null;
            }

            List<Note> notes = [];
            while (await reader.ReadAsync())
            {
                notes.Add(MapNote(reader));
            }

            return notes;
        }

        public static async Task<Note?> ReceiveNoteAsync(int id)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var command = dataSource.CreateCommand($"SELECT * FROM {_dbNotesTableName} WHERE id = {id}");
            await using var reader = await command.ExecuteReaderAsync();

            // Validate
            if (!reader.HasRows)
            {
                return null;
            }

            await reader.ReadAsync();
            return MapNote(reader);
        }

        public static async Task AddNote(string title, string text)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var command = dataSource.CreateCommand($"INSERT INTO {_dbNotesTableName} (title, content) VALUES('{title}', '{text}')");
            await command.ExecuteNonQueryAsync();
        }

        public static async Task DeleteNote(int id)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var command = dataSource.CreateCommand($"DELETE FROM {_dbNotesTableName} WHERE id = {id}");
            await command.ExecuteNonQueryAsync();
        }

        private static Note MapNote(NpgsqlDataReader reader)
        {
            return new() {
                Id      = (int)      reader["id"],
                Created = (DateTime) reader["created"],
                Title   = (string)   reader["title"],
                Content = (string)   reader["content"]
            };
        }
    }
}