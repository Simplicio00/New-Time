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
    public class ClassificadoController : ControllerBase
    {
        ClassificadoRepositorio repositorio = new ClassificadoRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<Classificado>>> Get() //definição do tipo de retorno
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
        public async Task<ActionResult<Classificado>> GetAction(int id)
        {
            Classificado classificadoRetornado = await repositorio.Get(id);
            if(classificadoRetornado == null)
            {
                return NotFound();
            }
            return classificadoRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Classificado>> Post(Classificado classificado) //tipo do objeto que está sendo enviado (Categoria) - nome que você determina pro objeto
        {
            try
            {
                await repositorio.Post(classificado);
            }
            catch (System.Exception)
            {
                throw;
            }
            return classificado;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Classificado>> Put(int id, Classificado classificado)
        {
            if(id != classificado.IdClassificado)
            {
                return BadRequest();
            }
            try
            {
               return await repositorio.Put(classificado);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                var classificadoValida = await repositorio.Get(id);
                if(classificadoValida == null)
                {
                    return NotFound();
                }else{
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Classificado>> Delete(int id)
        {
            Classificado classificadoRetornado = await repositorio.Get(id);
            if(classificadoRetornado == null)
            {
                return NotFound();
            }
            await repositorio.Delete(classificadoRetornado);
            return classificadoRetornado;
        }
    }
}