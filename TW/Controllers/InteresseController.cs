using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TW.Models;
using TW.Repositorios;

namespace TW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class InteresseController : ControllerBase
    {
        InteresseRepositorio repositorio = new InteresseRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<Interesse>>> Get() //definição do tipo de retorno
        {
            try
            {
                return await repositorio.Get();
                //await vai esperar traser a lista para armazenar em Categoria
            }
            catch (System.Exception)
            {
                throw;
            } 
            
        }       

        [HttpGet("{id}")]
        public async Task<ActionResult<Interesse>> GetAction(int id)
        {
            Interesse interesseRetornado = await repositorio.Get(id);
            if(interesseRetornado == null)
            {
                return NotFound();
            }
            return interesseRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Interesse>> Post(Interesse interesse) //tipo do objeto que está sendo enviado (Categoria) - nome que você determina pro objeto
        {
            try
            {
                await repositorio.Post(interesse);
            }
            catch (System.Exception)
            {
                throw;
            }
            return interesse;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Interesse>> Put(int id, Interesse interesse)
        {
            if(id != interesse.IdInteresse)
            {
                return BadRequest();
            }
            try
            {
               return await repositorio.Put(interesse);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                var interesseValido = await repositorio.Get(id);
                if(interesseValido == null)
                {
                    return NotFound();
                }else{
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Interesse>> Delete(int id)
        {
            Interesse interesseRetornado = await repositorio.Get(id);
            if(interesseRetornado == null)
            {
                return NotFound();
            }
            await repositorio.Delete(interesseRetornado);
            return interesseRetornado;
        }
    }
}