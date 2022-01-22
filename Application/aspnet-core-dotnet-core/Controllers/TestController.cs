


using aspnet_core_dotnet_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_dotnet_core.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string q = "select id, name from dbo.Test";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("database");
            SqlDataReader reader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand command = new SqlCommand(q, myCon))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}

