using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KursKayitUyelerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<KursKayitUyelerController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public KursKayitUyelerController(ILogger<KursKayitUyelerController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select kku.*, ku.KursAdi as KursAdi, ki.KisiAdi + ' ' + ki.KisiSoyadi as KursKayitUyeAdiSoyadi from KursKayitUyeler kku left join Kurslar ku on kku.KursId = ku.KursId left join Kisiler ki on kku.UyeId = ki.KisiId";
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
        public JsonResult Post(KursKayitUye kursKayitUye)
        {
            string query = @"
                    insert into DernekDB.dbo.KursKayitUyeler (KursId,UyeId,BaslangicTarih,BitisTarih) 
                    values('" + kursKayitUye.KursId + @"','" + kursKayitUye.UyeId + @"','" + kursKayitUye.BaslangicTarih + @"','" + kursKayitUye.BitisTarih + @"')
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
            return new JsonResult("KursKayitUye Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(KursKayitUye kursKayitUye)
        {
            string query = @"
                    Update DernekDB.dbo.KursKayitUyeler set KursId = '" + kursKayitUye.KursId + @"',UyeId='" + kursKayitUye.UyeId + @"',BaslangicTarih='" + kursKayitUye.BaslangicTarih + @"',BitisTarih='" + kursKayitUye.BitisTarih + @"'where KursKayitUyeId = '" + kursKayitUye.KursKayitUyeId + @"'
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
            return new JsonResult("KursKayitUye Guncellendi!");
        }

        [HttpDelete("{kursKayitUyeId}")]
        public JsonResult Delete(int kursKayitUyeId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.KursKayitUyeler where KursKayitUyeId = '" + kursKayitUyeId + @"'
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
                return new JsonResult("KursKayitUye Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
