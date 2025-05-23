﻿using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace Oscars.Data
{
	public class Database
	{

		private readonly string MySQLConnectionString;

		public Database()
		{
			//MySQLConnectionString = "Server=127.0.0.1; Database=employees; Uid=usrEmployees; Pwd=password;";
			//MySQLConnectionString = "Server=dynamicentertainment.se; Database=Oscars; Uid=OscarsServer; Pwd=Snygg1ng!;";
			MySQLConnectionString = "Server=192.168.1.101; Database=Oscars; Uid=OscarsServer; Pwd=Snygg1ng!;";
		}

		public async Task<DataTable> GetCeremonies()
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM Ceremonies ORDER BY ID DESC", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetNominations(int ceremonyId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM Nominations WHERE CeremonyId = " + ceremonyId, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetUsersParticipating(int ceremonyId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand("SELECT Users.ID,Users.Name,Users.ShortName,Ceremonies.ID AS CeremonyID FROM Users INNER JOIN UserParticipatingThisYear ON UserID=Users.ID INNER JOIN Ceremonies ON CeremonyID=Ceremonies.ID WHERE Ceremonies.ID=" + ceremonyId, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetCategories(int ceremonyId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT Categories.ID, Categories.Name FROM Nominations INNER JOIN Ceremonies ON Ceremonies.ID=CeremonyID AND CeremonyID={ceremonyId} INNER JOIN Categories ON CategoryID=Categories.ID GROUP BY CategoryID ORDER BY SortIndex", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetNominations(int ceremonyId, int categoryId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT Movies.*, Nominations.Nominee, Nominations.Winner, Nominations.ID as NomId FROM Nominations INNER JOIN Ceremonies ON CeremonyID=Ceremonies.ID AND Ceremonies.ID={ceremonyId} INNER JOIN Movies ON MovieID = Movies.ID WHERE CategoryID = {categoryId};", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetUsersSeen(int movieId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT ID,Name FROM Users INNER JOIN MovieSeen ON UserID=Users.ID AND MovieID={movieId}", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetUsersWant(int nomId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT ID,Name FROM Users INNER JOIN UserWantsToWin ON UserID=Users.ID AND NominationID={nomId}", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetUsersThink(int nomId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT ID,Name FROM Users INNER JOIN UserThinksWillWin ON UserID=Users.ID AND NominationID={nomId}", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetMovieWins(int movieId)
		{
			DataTable dt = new DataTable();
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT Winner, Categories.Name FROM Nominations INNER JOIN Categories ON Categories.ID = Nominations.CategoryID WHERE MovieID={movieId};", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task<DataTable> GetNumberOfNominations(int ceremonyId)
		{
			DataTable dt = new DataTable();

			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand selectCommand = new MySqlCommand($"SELECT Count(MovieId) AS NumNominations, COALESCE(SuM(Winner),0) AS Wins, WatchTogether, MovieId, Title, URL FROM Nominations INNER JOIN Movies ON Movies.ID=Nominations.MovieId WHERE CeremonyId={ceremonyId} GROUP BY MovieId ORDER BY NumNominations DESC, MovieId;", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				using (var rdr = await selectCommand.ExecuteReaderAsync())
				{
					dt.Load(rdr);
				}
				conn.Close();
			}
			return dt;
		}

		public async Task SetTogether(int movieId, bool together)
		{
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				// Read rows - Limit for testing purpose to 15 records
				MySqlCommand updateCommand = new MySqlCommand($"UPDATE Movies SET WatchTogether={(together ? 1 : 0)} WHERE ID={movieId}", conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				await updateCommand.ExecuteNonQueryAsync();
				conn.Close();
			}
		}

		public async Task SetMovieSeenByUser(int movieId, int userId, bool seen)
		{
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				string sql;
				if (seen)
					sql = $"INSERT INTO MovieSeen (MovieID, UserID) VALUES ({movieId}, {userId})";
				else
					sql = $"DELETE FROM MovieSeen WHERE MovieID={movieId} AND UserID={userId}";

				MySqlCommand updateCommand = new MySqlCommand(sql, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				await updateCommand.ExecuteNonQueryAsync();
				conn.Close();
			}
		}

		public async Task SetUserWants(int nomId, int userId, bool wants)
		{
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				string sql;
				if (wants)
					sql = $"INSERT INTO UserWantsToWin (NominationID, UserID) VALUES ({nomId}, {userId})";
				else
					sql = $"DELETE FROM UserWantsToWin WHERE NominationID={nomId} AND UserID={userId}";

				MySqlCommand updateCommand = new MySqlCommand(sql, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				await updateCommand.ExecuteNonQueryAsync();
				conn.Close();
			}
		}

		public async Task SetUserThinks(int nomId, int userId, bool thinks)
		{
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				string sql;
				if (thinks)
					sql = $"INSERT INTO UserThinksWillWin (NominationID, UserID) VALUES ({nomId}, {userId})";
				else
					sql = $"DELETE FROM UserThinksWillWin WHERE NominationID={nomId} AND UserID={userId}";

				MySqlCommand updateCommand = new MySqlCommand(sql, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				await updateCommand.ExecuteNonQueryAsync();
				conn.Close();
			}
		}

		public async Task SetAvailable(int movieId, string text)
		{
			using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
			{
				// Connect to the database
				conn.Open();
				// The MySqlCommand class represents a SQL statement to execute against a MySQL database
				string sql;
				sql = $"UPDATE Movies SET URL='{text}' WHERE ID={movieId}";

				MySqlCommand updateCommand = new MySqlCommand(sql, conn);
				// execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
				await updateCommand.ExecuteNonQueryAsync();
				conn.Close();
			}
		}
	}
}