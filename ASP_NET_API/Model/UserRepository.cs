using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ASP_NET_API.Model
{
    public interface IUserRepository
    {
        bool isAdmin(int userId);
        List<User> GetAll();
        User Get(int id);
        User Get(string email);
        User Get(string email, string password);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
    }
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public bool isAdmin(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [Users] WHERE id = {userId}";
                User user = db.Query<User>(sqlQuery, new { userId }).FirstOrDefault();
                return user != null && user.isAdmin;
            }
        }
        public List<User> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM [Users]").ToList();
            }
        }

        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [Users] WHERE id = {id}";
                return db.Query<User>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public User Get(string email)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [Users] WHERE email = \'{email}\'";
                return db.Query<User>(sqlQuery).FirstOrDefault();
            }
        }

        public User Get(string email, string hashPassword)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [Users] WHERE email = \'{email}\' and hashPassword = \'{hashPassword}\'";
                return db.Query<User>(sqlQuery).FirstOrDefault();
            }
        }

        public bool Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO [Users] ([email], [hashPassword], [isAdmin]) VALUES" +
                    $"(" +
                    $"\'{user.email}\', " +
                    $"\'{user.hashPassword}\', " +
                    $"{Convert.ToInt32(user.isAdmin)}" +
                    $")";
                if (db.Execute(sqlQuery, user) > 0)
                    return true;
                return false;
            }
        }

        public bool Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE [Users] SET " +
                    $"email = \'{user.email}\', " +
                    $"hashPassword = \'{user.hashPassword}\' " +
                    $"WHERE id = {user.id}";
                if (db.Execute(sqlQuery, user) > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [Users] WHERE id = {id}";
                if (db.Execute(sqlQuery, new { id }) > 0)
                    return true;
                return false;
            }
        }
    }
}