using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzinlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<IzinlerController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public IzinlerController(ILogger<IzinlerController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Izinler";
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
        public JsonResult Post(Izin izin)
        {
            string query = @"
                    insert into DernekDB.dbo.Izinler (KisiId,BaslangicTarih,BitisTarih,Aciklama) 
                    values('" + izin.KisiId + @"','" + izin.BaslangicTarih + @"','" + izin.BitisTarih + @"','" + izin.Aciklama + @"')
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
            return new JsonResult("Izin Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Izin izin)
        {
            string query = @"
                    Update DernekDB.dbo.Borclar set KisiId = '" + izin.KisiId + @"',BaslangicTarih='" + izin.BaslangicTarih + @"',BitisTarih='" + izin.BitisTarih + @"',Aciklama='" + izin.Aciklama + @"'where IzinId = '" + izin.IzinId + @"'
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
            return new JsonResult("Izin Guncellendi!");
        }

        [HttpDelete("{izinId}")]
        public JsonResult Delete(int izinId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Izinler where IzinId = '" + izinId + @"'
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
                return new JsonResult("İzin Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
