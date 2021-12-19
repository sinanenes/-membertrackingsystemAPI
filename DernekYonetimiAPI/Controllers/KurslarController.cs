using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurslarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<KurslarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public KurslarController(ILogger<KurslarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT ku.*,ki.KisiAdi +' '+ ki.KisiSoyadi as EgitmenAdi, ko.KodAdi as KursTurKodDeger FROM DernekDB.dbo.Kurslar ku left join DernekDB.dbo.Kisiler ki on ku.EgitmenId = ki.KisiId left join DernekDB.dbo.Kodlar ko on ku.KursTurKod = ko.KodDeger and ko.KodGrup = 'KursTurKod'";
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
        public JsonResult Post(Kurs kurs)
        {
            string query = @"
                    insert into DernekDB.dbo.Kurslar (KursAdi,KursTurKod,BaslangicTarih,BitisTarih,Kapasite,KursUcret,EgitmenId) 
                    values('" + kurs.KursAdi + @"','" + kurs.KursTurKod + @"','" + kurs.BaslangicTarih + @"','" + kurs.BitisTarih + @"','" + kurs.Kapasite + @"','" + kurs.KursUcret + @"','" + kurs.EgitmenId + @"')
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
            return new JsonResult("Kurs Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Kurs kurs)
        {
            string query = @"
                    Update DernekDB.dbo.Kurslar set KursAdi = '" + kurs.KursAdi + @"',KursTurKod = '" + kurs.KursTurKod + @"',BaslangicTarih='" + kurs.BaslangicTarih + @"',BitisTarih='" + kurs.BitisTarih + @"',Kapasite='" + kurs.Kapasite+ @"',KursUcret='" + kurs.KursUcret + @"',EgitmenId='" + kurs.EgitmenId + @"'where KursId = '" + kurs.KursId + @"'
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
            return new JsonResult("Kurs Guncellendi!");
        }

        [HttpDelete("{kursId}")]
        public JsonResult Delete(int kursId) //Delete methodunu duzelt daha sonra kisiyi silmeden sadece pasife cek!
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Kurslar where KursId = '" + kursId + @"'
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
                return new JsonResult("Kurs Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
        }

        [Route("GetAllKursTurleri")]
        [HttpGet]
        public JsonResult GetAllKursTurleri()
        {
            string query = @"select * from DernekDB.dbo.Kodlar where KodGrup='KursTurKod'";
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
    }
}
