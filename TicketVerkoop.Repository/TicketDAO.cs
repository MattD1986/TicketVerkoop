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
    public class TicketDAO : IDAO<Ticket>
    {
        private readonly ProLeagueDbContext _dbContext;

        public TicketDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            try
            {
                return await _dbContext.Tickets
                            .Include(t => t.Order)
                            .Include(t => t.Plaats)
                            .Include(t => t.Wedstrijd)
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Ticket");
            }
        }

        public async Task<Ticket> FindById(int id)
        {
            try
            {
                return await _dbContext.Tickets
                         .Where(t => t.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Ticket"); }
        }

        public async Task Add(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Ticket");
            }
        }

        public async Task Update(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Ticket");
            }
        }

        public async Task Delete(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Ticket");
            }
        }
    }
}