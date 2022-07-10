using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ASP_NET_API.Model
{
    public interface IAdvertisementRepository
    {
        List<Advertisement> GetAll();
        Advertisement Get(int id);
        bool Create(Advertisement ad);
        bool Update(Advertisement ad);
        bool Delete(int id);
    }
    public class AdvertisementRepository : IAdvertisementRepository
    {
        string connectionString = null;
        public AdvertisementRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public List<Advertisement> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Advertisement>("SELECT * FROM [ad]").ToList();
            }
        }

        public Advertisement Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [ad] WHERE id = {id}";
                return db.Query<Advertisement>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public bool Create(Advertisement ad)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO [ad] ([url], title, [description], price, [date], regionId, headingId, vip) VALUES" +
                    $"(" +
                    $"\'{ad.url}\', " +
                    $"N\'{ad.title}\', " +
                    $"N\'{ad.description}\', " +
                    $"\'{ad.price}\', " +
                    $"\'{ad.date}\', " +
                    $"{ad.regionId}, " +
                    $"{ad.headingId}, " +
                    $"{Convert.ToInt32(ad.vip)}" +
                    $")";
                if (db.Execute(sqlQuery, ad) > 0)
                    return true;
                return false;
            }
        }

        public bool Update(Advertisement ad)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE [ad] SET " +
                    $"title = N\'{ad.title}\', " +
                    $"[description] = N\'{ad.description}\', " +
                    $"[url] = \'{ad.url}\', " +
                    $"[price] = {ad.price}, " +
                    $"[date] = \'{ad.date}\', " +
                    $"[vip] = {Convert.ToInt32(ad.vip)}, " +
                    $"[regionId] = {ad.regionId}, " +
                    $"[headingId] = {ad.headingId} " +
                    $"WHERE id = {ad.id}";
                if (db.Execute(sqlQuery, ad) > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [ad] WHERE id = {id}";
                if (db.Execute(sqlQuery, new { id }) > 0)
                    return true;
                return false;
            }
        }
    }
}