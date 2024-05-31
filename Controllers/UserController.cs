using MarcaTento.Data;
using MarcaTento.Extensions;
using MarcaTento.Models;
using MarcaTento.ViewModels;
using MarcaTento.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcaTento.Controllers
{
    /*  
      Ao contrário da classe Controller, o ControllerBase
    implementa uma classe SEM o suporte para VIEW do MVC
    (Model, View, Controll). A classe ControllerBase é mais
    enxuta e simples.
     */
    [ApiController] // essa linha é MUITO IMPORTANTE NÃO ESQUEÇA    
    public class UserController : ControllerBase 
    {
        /*
           Criação do método Get de forma assíncrona.
          Primeiro recebemos o contexto do banco de dados,
        depois pegamos os usuários desse contexto e jogamos
        em uma lista.
         */
        [HttpGet("v1/users")]
        public async Task<IActionResult> GetAsync( // MÉTODO GET
            [FromServices] MarcaTentoDataContext context)
        {
            try
            {
                var users = await context
                    .Users
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(new ResultViewModel<List<User>>(users));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<User>>("Falha interna no servidor"));
            }
            
        }

        /*
           Criação do método GetById de forma assíncrona.
          Primeiro recebemos o contexto do banco de dados, em
        segundo recebemos o id pela rota para identificar qual
        registro queremos, depois pegamos o usuário desse 
        contexto.
         */
        [HttpGet("v1/users/{id:int}")]
        public async Task<IActionResult> GetByIdAsync( // MÉTODO GET BY ID
            [FromRoute] int id,
            [FromServices] MarcaTentoDataContext context)
        {
            try
            {
                var model = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                return Ok(new ResultViewModel<User>(model));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }


        [HttpGet("v1/users/{id:int}/matches")]
        public async Task<IActionResult> GetByIdWithMatchesAsync( // MÉTODO GET BY ID WITH USER MATCHES
            [FromRoute] int id,
            [FromServices] MarcaTentoDataContext context)
        {
            try
            {
                var model = await context
                    .Users
                    .Include(x => x.Matches)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (model == null) 
                    return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                return Ok(new ResultViewModel<User>(model));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }

        /*
           Criação do método Post de forma assíncrona.
          Primeiro recebemos o contexto do banco de dados, em
        segundo recebemos o objeto a ser criado em padrão JSON, 
        depois inserimos o usuário desse contexto.
         */
        [HttpPost("v1/users")]
        public async Task<IActionResult> PostAsync( // MÉTODO POST
            [FromBody] EditorUserViewModel model,
            [FromServices] MarcaTentoDataContext context) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            try
            {
                var user = new User
                {
                    Id = 0,
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash,
                    Image = model.Image,
                    Matches = [],
                    Slug = model.Username.Replace(" ","-").ToLower()
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"v1/users/{user.Id}", new ResultViewModel<User>(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível inserir o usuário"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha inserir no servidor"));
            }
        }

        /*
           O método Put é o mais complicado, pois precisamos receber
        o contexto, o id do objeto que vamos mudar e o novo objeto
        que será inserido no lugar dele, como se fosse uma substituição.
         É interessante criarmos diferentes views tanto no Put e no Post
        para que diferentes parâmetros possam ser passados. Como a edição
        de apenas um campo dentro do entidade do usuário e etc.
         */
        [HttpPut("v1/users/{id:int}")]
        public async Task<IActionResult> PutAsync( // MÉTODO PUT
            [FromRoute] int id,
            [FromBody] EditorUserViewModel model,
            [FromServices] MarcaTentoDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null) 
                    return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                user.Username = model.Username;
                user.Email = model.Email;
                user.PasswordHash = model.PasswordHash;
                user.Image = model.Image;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível alterar o usuário"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }

        }

        [HttpDelete("v1/users/{id:int}")]
        public async Task<IActionResult> DeleteAsync( // METODO DELETE
            [FromRoute] int id,
            [FromServices] MarcaTentoDataContext context)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null) return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }


        /*
        {
            "id": 1,
            "slug": "joao-xarrua",
            "name": "João Xarrua",
            "email": "joaoxarrua@mail.com",
            "password": "1234",
            "image": "/imagens/profilepic.jpg",
            "matches": null
        }
        */

    }
}
