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
    public class StadionDAO : IDAO<Stadion>
    {

        private readonly ProLeagueDbContext _dbContext;

        public StadionDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }
        public async Task<IEnumerable<Stadion>> GetAll()
        {
            try
            {
                return await _dbContext.Stadions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Stadion");
            }
        }

        public async Task<Stadion> FindById(int id)
        {
            try
            {
                return await _dbContext.Stadions
                         .Where(s => s.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Stadion"); }
        }

        public async Task Add(Stadion entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Stadion");
            }
        }

        public async Task Update(Stadion entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Stadion");
            }
        }

        public async Task Delete(Stadion entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Stadion");
            }
        }
    }
}