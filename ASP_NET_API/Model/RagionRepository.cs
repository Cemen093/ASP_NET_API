using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ASP_NET_API.Model
{
    public interface IRagionRepository
    {
        List<Region> GetAll();
        Region Get(int id);
        bool Create(Region region);
        bool Update(Region region);
        bool Delete(int id);
        bool Init();
    }
    public class RagionRepository : IRagionRepository
    {
        string connectionString = null;
        public RagionRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public bool Init()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"CREATE TABLE [Users]([id] int identity primary key,[email] nvarchar(250),[hashPassword] nvarchar(250),[isAdmin] bit,)";
                sqlQuery += "CREATE TABLE [region]([id] int identity primary key,[title] nvarchar(250))";
                sqlQuery += "CREATE TABLE [heading]([id] int identity primary key,[title] nvarchar(250),[url] nvarchar(max))";
                sqlQuery += "CREATE TABLE [ad]([id] int identity primary key,[title] nvarchar(250),[url] nvarchar(max),[description] nvarchar(max),[price] int,[date] date,[vip] bit,[regionId] int,[headingId] int,FOREIGN KEY([regionId]) REFERENCES[region]([id]),FOREIGN KEY([headingId]) REFERENCES[heading]([id]))";
                sqlQuery += "INSERT INTO [region] VALUES (N'Одесская'), (N'Днепропетровская'), (N'Черниговская'),(N'Харьковская'), (N'Житомирская'), (N'Полтавская'),(N'Херсонская'), (N'Киевская'), (N'Запорожская'),(N'Луганская'), (N'Донецкая'), (N'Винницкая'),(N'Кировоградская'), (N'Николаевская'), (N'Сумская'),(N'Львовская'), (N'Черкасская'), (N'Хмельницкая'),(N'Волынская'), (N'Ровенская'), (N'Ивано-Франковская'),(N'Тернопольская'),(N'Закарпатская'), (N'Черновицкая')";
                sqlQuery += "INSERT INTO [heading] (url, title) VALUES ('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/5d9ab47d205c9b353e01ab2db716eaf1.png', N'Детский мир'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/f9dd340a12a3db178a80ff26169d1010.png', N'Недвижимость'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/b7209239b859b7480bcb3e21c817497a.png', N'Авто'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/e22d28731ba369a96caabec5002aa763.png', N'Запчасти'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/0ecb22062276d5d45b662ece07d29c64.png', N'Работа'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/b1e215e1cfe61392d827aad766b44f5e.png', N'Животные'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/10beec301ca5cf3ae30db61702e7e4ca.png', N'Дом и сад'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/eb5e8444c9152266ab130a84a09e3c07.png', N'Электроника'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/2431d6fbbdafd742079265fbdd46c901.png', N'Бизнес'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/c2c2d81278bf0aae91e311c0863e188b.png', N'Мода'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/19d0f0aec32dd8ae6143d99db680b0ed.png', N'Хобби'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/f910473974d0298aecff3f806a2306dd.png', N'Халява'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/10909ab2cdca7b8ce281e3e05771e59f.png', N'Обмен'),('https://s1.hostingkartinok.com/uploads/thumbs/2022/06/c25b726e71047b0863a5f2bbb06c752d.png', N'Работа')";
                sqlQuery += "INSERT INTO [ad] ([url], title, [description], price, [date], regionId, headingId, vip) VALUES ('https://ireland.apollo.olxcdn.com/v1/files/mibnl7tl0cpe3-UA/image;s=644x461', N'Армированная пленка для прудов и озер', 'none', 125, N'01.01.2022', 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/og5g0rk7er182-UA/image;s=644x461', N'Комиксы ,манга,Робин Хобб', 'none', 300 , GETDATE(), 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/4ktecc3ba6im1-UA/image;s=644x461', N'Ручка переноса огня АК АК74 тактическая ручка переноса', 'none', 400 , GETDATE(), 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/bhj34ga0oye2-UA/image;s=644x461', N'Квартира 4 ком. 522-мкрн.5 мин. м.Студенческая,квадратный холл', 'none', 61500 , GETDATE(), 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/jxx9zqd6k6272-UA/image;s=644x461', N'Дом с гаражом Фарбоване', 'none', 500000 , N'02.01.2022', 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/er0tzh9be98x1-UA/image;s=644x461', N'клетки для бройлеров и кур-несушек', 'none', 1500 , GETDATE(), 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/kz51zqy0q0242-UA/image;s=644x461', N'Покраска автомобилей', 'none', 0 , GETDATE(), 1, 1, 1),('https://ireland.apollo.olxcdn.com/v1/files/wi9cf8blgad11-UA/image;s=644x461', N'Стеллаж. Loft. Лофт. Офисная мебель.', 'none', 999 , N'03.04.2022', 1, 1, 1)";
                
                if (db.Execute(sqlQuery) > 0)
                    return true;
                return false;
            }
        }
        public List<Region> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Region>("SELECT * FROM [region]").ToList();
            }
        }

        public Region Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM [region] WHERE id = {id}";
                return db.Query<Region>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public bool Create(Region region)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO [region] VALUES(\'{region.title}\')";
                if (db.Execute(sqlQuery, region) > 0)
                    return true;
                return false;
            }
        }

        public bool Update(Region region)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE [region] SET title = \'{region.title}\' WHERE id = {region.id}";
                if (db.Execute(sqlQuery, region) > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [region] WHERE id = {id}";
                if (db.Execute(sqlQuery, new { id }) > 0)
                    return true;
                return false;
            }
        }
    }
}