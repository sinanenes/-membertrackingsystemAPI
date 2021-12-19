using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BilgilendirmelerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<BilgilendirmelerController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BilgilendirmelerController(ILogger<BilgilendirmelerController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Bilgilendirmeler";
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
        public JsonResult Post(Bilgilendirme bilgilendirme)
        {
            string query = @"
                    insert into DernekDB.dbo.Bilgilendirmeler (BilgilendirmeMetni,BaslangicTarih,BitisTarih,Aciklama) 
                    values('" + bilgilendirme.BilgilendirmeMetni + @"','" + bilgilendirme.BaslangicTarih + @"','" + bilgilendirme.BitisTarih + @"','" + bilgilendirme.Aciklama + @"')
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
            return new JsonResult("Bilgilendirme Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Bilgilendirme bilgilendirme)
        {
            string query = @"
                    Update DernekDB.dbo.Bilgilendirmeler set BilgilendirmeMetni = '" + bilgilendirme.BilgilendirmeMetni + @"',BaslangicTarih='" + bilgilendirme.BaslangicTarih + @"',BitisTarih='" + bilgilendirme.BitisTarih + @"',Aciklama='" + bilgilendirme.Aciklama + @"'where BilgilendirmeId = '" + bilgilendirme.BilgilendirmeId + @"'
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
            return new JsonResult("Bilgilendirme Guncellendi!");
        }

        [HttpDelete("{bilgilendirmeId}")]
        public JsonResult Delete(int bilgilendirmeId) //Delete methodunu duzelt daha sonra kisiyi silmeden sadece pasife cek!
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Bilgilendirmeler where BilgilendirmeId = '" + bilgilendirmeId + @"'
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
                return new JsonResult("Bilgilendirme Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
