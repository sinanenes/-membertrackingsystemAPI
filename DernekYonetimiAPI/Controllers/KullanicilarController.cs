using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullanicilarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<KullanicilarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public KullanicilarController(ILogger<KullanicilarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Kullanicilar";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        [HttpGet("{kullaniciAdi}/{parola}")]
        public JsonResult Get(string kullaniciAdi, string parola)
        {
            string query = @"SELECT * FROM DernekDB.dbo.Kullanicilar where AktifPasifKod = 1 and KullaniciAdi = '" + kullaniciAdi + @"'and Parola = '" + parola + @"'
                    ";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post(Kullanici kullanici)
        {
            string query = @"
                    insert into DernekDB.dbo.Kullanicilar (KullaniciAdi,KullaniciTipiKod,AktifPasifKod,Parola) 
                    values('" + kullanici.KullaniciAdi + @"','" + kullanici.KullaniciTipiKod + @"','" + kullanici.AktifPasifKod + @"','" + kullanici.Parola + @"')
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Kullanici Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Kullanici kullanici)
        {
            string query = @"
                    Update DernekDB.dbo.Kullanicilar set KullaniciAdi = '" + kullanici.KullaniciAdi + @"',KullaniciTipiKod = '" + kullanici.KullaniciTipiKod + @"',AktifPasifKod='" + kullanici.AktifPasifKod + @"',Parola='" + kullanici.Parola + @"'where KullaniciId = '" + kullanici.KullaniciId + @"'
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Kullanici Guncellendi!");
        }

        [HttpDelete("{kullaniciId}")]
        public JsonResult Delete(int kullaniciId) //Delete methodunu duzelt daha sonra kisiyi silmeden sadece pasife cek!
        {
            string query = @"
                    Delete from DernekDB.dbo.Kullanicilar where KullaniciId = '" + kullaniciId + @"'
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Kullanici Silindi!");
        }


    }
}
