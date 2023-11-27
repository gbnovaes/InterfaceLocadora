using Locadora.Models;
using Microsoft.EntityFrameworkCore;


namespace Locadora.Interfaces
{
    public interface ILocadoraContext 
    {
        DbSet<Filme> Filmes { get; set; }
        Task<int> SaveChangesAsync();
        // Novos métodos para CRUD
        Task<Filme> GetFilmeByIdAsync(long id);
        Task AddFilmeAsync(Filme filme);
        Task UpdateFilmeAsync(Filme filme);
        Task DeleteFilmeAsync(long id);
    }

}
