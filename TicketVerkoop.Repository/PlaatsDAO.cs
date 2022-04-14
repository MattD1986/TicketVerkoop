using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Domain.Data;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Repository.Interfaces;

namespace TicketVerkoop.Repository
{
    public class PlaatsDAO : IDAO<Plaat>
    {
        private readonly ProLeagueDbContext _dbContext;

        public PlaatsDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Plaat>> GetAll()
        {
            try
            {
                return await _dbContext.Plaats
                            .Include(p => p.Vak)
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Plaats");
            }
        }

        public async Task<Plaat> FindById(int id)
        {
            try
            {
                return await _dbContext.Plaats
                         .Where(p => p.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Plaats"); }
        }

        public async Task Add(Plaat entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Plaats");
            }
        }

        public async Task Update(Plaat entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Plaats");
            }
        }

        public async Task Delete(Plaat entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Plaats");
            }
        }
    }
}