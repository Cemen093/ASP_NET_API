using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ASP_NET_API.Model
{
    public interface IHeadingRepository
    {
        List<Heading> GetAll();
        Heading Get(int id);
        bool Create(Heading heading);
        bool Update(Heading heading);
        bool Delete(int id);
    }
    public class HeadingRepository : IHeadingRepository
    {
        string connectionString = null;
        public HeadingRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public List<Heading> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Heading>("SELECT * FROM heading").ToList();
            }
        }

        public Heading Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM heading WHERE id = {id}";
                return db.Query<Heading>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public bool Create(Heading heading)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO heading VALUES(\'{heading.title}\', \'{heading.url}\')";
                if (db.Execute(sqlQuery, heading) > 0)
                    return true;
                return false;
            }
        }

        public bool Update(Heading heading)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE heading SET title = \'{heading.title}\', url = \'{heading.url}\' WHERE id = {heading.id}";
                if (db.Execute(sqlQuery, heading) > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM heading WHERE id = {id}";
                if (db.Execute(sqlQuery, new { id }) > 0)
                    return true;
                return false;
            }
        }
    }
}