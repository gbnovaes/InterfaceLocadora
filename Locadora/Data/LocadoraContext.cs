using Locadora.Interfaces;
using Locadora.Models;
using Microsoft.EntityFrameworkCore;


namespace Locadora.Data
{
    public class LocadoraContext : DbContext, ILocadoraContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options) { }
        public DbSet<Filme> Filmes { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public async Task<Filme> GetFilmeByIdAsync(long id)
        {
            return await Filmes.FindAsync(id);
        }

        public async Task AddFilmeAsync(Filme filme)
        {
            Filmes.Add(filme);
            await SaveChangesAsync();
        }

        public async Task UpdateFilmeAsync(Filme filme)
        {
            Entry(filme).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteFilmeAsync(long id)
        {
            var filme = await Filmes.FindAsync(id);
            Filmes.Remove(filme);
            await SaveChangesAsync();
        }
    }
}
