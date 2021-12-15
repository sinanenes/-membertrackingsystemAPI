using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasaBankalarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<KasaBankalarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public KasaBankalarController(ILogger<KasaBankalarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.KasaBankalar";
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
        public JsonResult Post(KasaBanka kasaBanka)
        {
            string query = @"
                    insert into DernekDB.dbo.KasaBankalar (KasaBankaAdi,KasaBankaTurKod,Aciklama) 
                    values('" + kasaBanka.KasaBankaAdi + @"','" + kasaBanka.KasaBankaTurKod + @"','" + kasaBanka.Aciklama+ @"')
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
            return new JsonResult("KasaBanka Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(KasaBanka kasaBanka)
        {
            string query = @"
                    Update DernekDB.dbo.KasaBankalar set KasaBankaAdi = '" + kasaBanka.KasaBankaAdi + @"',KasaBankaTurKod='" + kasaBanka.KasaBankaTurKod + @"',Aciklama='" + kasaBanka.Aciklama + @"'where KasaBankaId = '" + kasaBanka.KasaBankaId + @"'
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
            return new JsonResult("KasaBanka Guncellendi!");
        }

        [HttpDelete("{kasaBankaId}")]
        public JsonResult Delete(int kasaBankaId)
        {
            string query = @"
                    Delete from DernekDB.dbo.KasaBankalar where KasaBankaId = '" + kasaBankaId + @"'
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
            return new JsonResult("KasaBanka Silindi!");
        }
    }
}
