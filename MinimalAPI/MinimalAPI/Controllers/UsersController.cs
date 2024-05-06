using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;
using MinimalAPI.Repositories;
using System.Text.Json.Nodes;

namespace MinimalAPI.Controllers
{
    // Controlador de la tabla Usuario
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Instanciación de la clase UsuariosRepository.
        private readonly UsersRepository usuariosRepository;
        public UsersController(UsersRepository usuariosRepository)
        {
            this.usuariosRepository = usuariosRepository;
        }

        // Método GET para recuperar todos los usuarios de la tabla.
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            return Ok(await usuariosRepository.GetAllUsers());

        }

        // Método GET para recuperar un usuario en concreto de la tabla.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioNombre(int id)
        {
            return Ok(await usuariosRepository.GetUser(id));
        }

        // Método POST para crear un usuario e insertarlo en la tabla.
        [HttpPost]
        public async Task<IActionResult> CreateUsuarioWeb([FromBody] NoIdUser usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insert = await usuariosRepository.InsertUser(usuario);
            return Created("Creado!", insert);
        }

        // Método PUT para actualizar un usuario concreto de la tabla.
        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] User usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var update = await usuariosRepository.UpdateUser(usuario);
            return Created("Actualizado!", update); 
        }

        // Método DELETE para eliminar un usuario concreto de la tabla.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await usuariosRepository.DeleteUser(new User { id = id });
            return Created("Eliminado!", deleted);
        }
    }
}
