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
    public class clubDAO : IDAO<Club>
    {
        private readonly ProLeagueDbContext _dbContext;

        public clubDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            try
            {
                return await _dbContext.Clubs.Include(c => c.Stadion).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO clubs");
            }
        }

        public async Task<Club> FindById(int id)
        {
            try
            {
                return await _dbContext.Clubs
                         .Where(c => c.ClubId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO clubs"); }
        }

        public async Task Add(Club entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO clubs");
            }
        }

        public async Task Update(Club entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO clubs");
            }
        }

        public async Task Delete(Club entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO clubs");
            }
        }
    }
}