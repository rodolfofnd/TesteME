using System.Threading.Tasks;

namespace MercadoEletronico.API.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}