using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EjercicioAPI.Data;
using EjercicioAPI.Models;

namespace EjercicioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.IdUsuario }, usuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }

        SqlConnection conn;
        private readonly IConfiguration _configuration;



        [HttpPost, Route("[action]", Name = "Login")]
        public UserDetails Login(Usuarios users)
        {
            UserDetails userdetails = new UserDetails();
            userdetails.result = new Result();
            try
            {
                if (users != null && !string.IsNullOrWhiteSpace(users.Usuario) && !string.IsNullOrWhiteSpace(users.Password))
                {
                    conn = new SqlConnection(_configuration["ConnectionStrings:sql-conn"]);
                    using (conn)
                    {
                        SqlCommand cmd = new SqlCommand("sp_users", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", users.Usuario);
                        cmd.Parameters.AddWithValue("@password", users.Password);
                        cmd.Parameters.AddWithValue("@stmttype", "userlogin");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            userdetails.Usuario = dt.Rows[0]["nombre"].ToString();
                            userdetails.Password = dt.Rows[0]["email"].ToString();

                            userdetails.result.result = true;
                            userdetails.result.message = "success";
                        }
                        else
                        {
                            userdetails.result.result = false;
                            userdetails.result.message = "Invalid user";
                        }
                    }
                }
                else
                {
                    userdetails.result.result = false;
                    userdetails.result.message = "Please enter username and password";
                }
            }
            catch (Exception ex)
            {
                userdetails.result.result = false;
                userdetails.result.message = "Error occurred: " + ex.Message.ToString();
            }
            return userdetails;
        }
    }
}
