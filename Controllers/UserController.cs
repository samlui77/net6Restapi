using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ytRESTfulAPI.Models;
using ytRESTfulAPI.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ytRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly DataContext _context;
        public UserController(DataContext dataCtxt)
        {
            _context = dataCtxt;
          //  config = _configuration;
            //_connStr = config.GetConnectionString("SourceConnectString");
        }

        [HttpGet]
        public async Task<ActionResult<List<ST_USER>>> Get()
        {
            return Ok(await _context.ST_USER
                            //include
                           // .Where(u => u.LoginName == "aaa")
                            .ToListAsync()
                     );
        }

      /*  public async Task<ActionResult<List<ST_USER>>> GetAll()
        {
            
            List<ST_USER> userList = _context.ST_USER.FromSqlRaw("EXECUTE ")
            .ToList();

             //return Ok(await _context.ST_USER.ToListAsync());
        }*/


        [HttpGet("{id}")]
        public async Task<ActionResult<ST_USER>> Get(int id)
        {
             List<ST_USER> users = new List<ST_USER>();
                
            var user = await _context.ST_USER.FindAsync(id);
            if (user == null)
                return BadRequest("No such user.");    
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<ST_USER>>> CreateUser(ST_USER user)
        {
            _context.ST_USER.Add(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.ST_USER.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ST_USER>>> UpdateUser(ST_USER request)
        {
            var dbUser = await _context.ST_USER.FindAsync(request.UserId);
            if (dbUser == null)
                return BadRequest("No such user.");
            
            dbUser.LoginName = request.LoginName;
            
            await _context.SaveChangesAsync();
            
            return Ok(await _context.ST_USER.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<ST_USER>>> DeleteUser(int id)
        {
            var dbUser = await _context.ST_USER.FindAsync(id);
            if (dbUser == null)
                return BadRequest("No such user.");

            _context.ST_USER.Remove(dbUser);

            await _context.SaveChangesAsync();
            return Ok(await _context.ST_USER.ToListAsync());
        }
       

    }
}
