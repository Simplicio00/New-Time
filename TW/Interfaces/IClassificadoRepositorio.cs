using System.Collections.Generic;
using System.Threading.Tasks;
using TW.Models;

namespace TW.Interfaces
{
    public interface IClassificadoRepositorio
      {
        Task<List<Classificado>> Get();
        Task<Classificado> Get(int id);
        Task<Classificado> Post(Classificado classificado);
        Task<Classificado> Put(Classificado classificado);
        Task<Classificado> Delete(Classificado classificadoRetornado);
    }
}