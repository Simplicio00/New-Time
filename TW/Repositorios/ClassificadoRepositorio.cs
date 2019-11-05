using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TW.Interfaces;
using TW.Models;

namespace TW.Repositorios
{
    public class ClassificadoRepositorio : IClassificadoRepositorio
    {
        TwContext context = new TwContext();
        public async Task<Classificado> Delete(Classificado classificadoRetornado)
        {
            context.Classificado.Remove(classificadoRetornado);
            await context.SaveChangesAsync();
            return classificadoRetornado;
        }

        public async Task<List<Classificado>> Get()
        {
            return await context.Classificado.ToListAsync();

        }

        public async Task<Classificado> Get(int id)
        {
            return await context.Classificado.FindAsync(id);
        }

        public async Task<Classificado> Post(Classificado classificado)
        {
            await context.Classificado.AddAsync(classificado);
            await context.SaveChangesAsync();
            return classificado;
        }

        public async Task<Classificado> Put(Classificado classificado)
        {
            context.Entry(classificado).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return classificado;
        }
    }
}