using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TahsilatlarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<TahsilatlarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TahsilatlarController(ILogger<TahsilatlarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Tahsilatlar";
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
        public JsonResult Post(Tahsilat tahsilat)
        {
            string query = @"
                    insert into DernekDB.dbo.Tahsilatlar (KisiId,TahsilatTarih,DonemAyKod,DonemYilKod,TahsilatTurKod,TahsilatTutar,KasaBankaId,Aciklama) 
                    values('" + tahsilat.KisiId + @"','" + tahsilat.TahsilatTarih + @"','" + tahsilat.DonemAyKod + @"','" + tahsilat.DonemYilKod + @"','" + tahsilat.TahsilatTurKod + @"','" + tahsilat.TahsilatTutar + @"','" + tahsilat.KasaBankaId + @"','" + tahsilat.Aciklama + @"')
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
            return new JsonResult("Tahsilat Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Tahsilat tahsilat)
        {
            string query = @"
                    Update DernekDB.dbo.Tahsilatlar set KisiId = '" + tahsilat.KisiId + @"',TahsilatTarih='" + tahsilat.TahsilatTarih + @"',DonemAyKod='" + tahsilat.DonemAyKod + @"',DonemYilKod='" + tahsilat.DonemYilKod + @"',TahsilatTurKod='" + tahsilat.TahsilatTurKod + @"',TahsilatTutar='" + tahsilat.TahsilatTutar + @"',KasaBankaId='" + tahsilat.KasaBankaId + @"',Aciklama='" + tahsilat.Aciklama + @"'where TahsilatId = '" + tahsilat.TahsilatId + @"'
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
            return new JsonResult("Tahsilat Guncellendi!");
        }

        [HttpDelete("{tahsilatId}")]
        public JsonResult Delete(int tahsilatId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Tahsilatlar where TahsilatId = '" + tahsilatId + @"'
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
                return new JsonResult("Tahsilat Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
