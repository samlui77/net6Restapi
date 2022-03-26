using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using ytRESTfulAPI.Models;

namespace ytRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly IConfiguration _config;
        private string _connStr;

        public DeptController(IConfiguration _configuration)
        {
            _config = _configuration;
            _connStr = _config.GetConnectionString("SQLConnStr");
        }

      
        [HttpGet]
        public async Task<ActionResult<List<Dept>>> Get()
        {
            string strSelectQuery = "SELECT id, compId, cname, ename FROM HR_CODE_DEPT";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmdDR = new SqlCommand(strSelectQuery, conn);
            cmdDR.CommandTimeout = 30; // 30 seconds
            List<Dept> deptList = new List<Dept>();
            try
             {
                //conn.Open();
                await conn.OpenAsync();
                SqlDataReader objDR = await cmdDR.ExecuteReaderAsync();
               
                //while (objDR.Read())
                while (await objDR.ReadAsync()) 
                {
                    Dept objDept = new();
                    objDept.Id = Convert.ToInt32(objDR["id"]);
                    objDept.CompId = Convert.ToInt32(objDR["compId"]);
                    objDept.EName = objDR["ename"].ToString() ?? "";
                    objDept.CName = objDR["cname"].ToString();
                    
                    deptList.Add(objDept);
                } // end while

                 objDR.Close();
             }
             catch (Exception ex)
             {  
                return BadRequest(ex.ToString());
             }
             finally
             {
                 conn.Close();
                 conn.Dispose();
             }
             return Ok(deptList);

          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dept>> Get(int id)
        {
            string strSelectQuery = "SELECT id, compId, cname, ename FROM HR_CODE_DEPT WHERE id = @id";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmdDR = new SqlCommand(strSelectQuery, conn);
            cmdDR.CommandTimeout = 30; // 30 seconds
            cmdDR.Parameters.Add("@id", SqlDbType.Int).Value = id;


            List<Dept> deptList = new();
            try
            {
                //conn.Open();
                await conn.OpenAsync();
                SqlDataReader objDR = await cmdDR.ExecuteReaderAsync();

                Dept objDept = new();

                if (await objDR.ReadAsync())
                {                    
                    objDept.Id = Convert.ToInt32(objDR["id"]);
                    objDept.CompId = Convert.ToInt32(objDR["compId"]);
                    objDept.EName = objDR["ename"].ToString() ?? "";
                    objDept.CName = objDR["cname"].ToString();

                    deptList.Add(objDept);
                } // end if

                objDR.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return Ok(deptList);
        }

        [HttpPut]
        public async Task<ActionResult<Dept>> UpdateDept(Dept request)
        {
            //List<Dept> deptList = new();
            //deptList.Add(request);

            string strSQL = "UPDATE HR_CODE_DEPT SET ";
                    strSQL += "eName = @eName, ";
                    strSQL += "cName = @cName, ";
                    strSQL += "chgTime = getdate() ";
                    strSQL += "WHERE  ";
                    strSQL += "id = @id ";
            SqlConnection conn = new(_connStr);
            try
            {
                SqlCommand UpdateCommand = new SqlCommand(strSQL, conn);
                    UpdateCommand.CommandTimeout = 30; // 30 seconds
                    UpdateCommand.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    UpdateCommand.Parameters.Add("@eName", SqlDbType.NVarChar, 50).Value = request.EName;
                    UpdateCommand.Parameters.Add("@cName", SqlDbType.NVarChar, 20).Value = request.CName;

                await conn.OpenAsync();
                await UpdateCommand.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return Ok(request);

        }

    }
}
