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
    public class VakDAO : IDAO<Vak>
    {

        private readonly ProLeagueDbContext _dbContext;

        public VakDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }
        public async Task<IEnumerable<Vak>> GetAll()
        {
            try
            {
                return await _dbContext.Vaks
                    .Include(v => v.Stadion)
                    .Include(v => v.VakOmschrijving)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Vak");
            }
        }

        public async Task<Vak> FindById(int id)
        {
            try
            {
                return await _dbContext.Vaks
                         .Where(s => s.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Vak"); }
        }

        public async Task Add(Vak entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Vak");
            }
        }

        public async Task Update(Vak entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Vak");
            }
        }

        public async Task Delete(Vak entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Vak");
            }
        }
    }
}