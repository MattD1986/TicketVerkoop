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
    public class WedstrijdDAO : IDAO<Wedstrijd>
    {
        private readonly ProLeagueDbContext _dbContext;

        public WedstrijdDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Wedstrijd>> GetAll()
        {
            try
            {
                return await _dbContext.Wedstrijds
                    .Include(w => w.ThuisPloegNavigation)
                    .Include(w => w.UitPloegNavigation)
                    .Include(w => w.Competitie)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Wedstrijd");
            }
        }

        public async Task<Wedstrijd> FindById(int id)
        {
            try
            {
                 return await _dbContext.Wedstrijds
                         .Where(w => w.Id == id)
                         .Include(w => w.ThuisPloegNavigation)
                         .Include(w => w.UitPloegNavigation)
                         .Include(w => w.ThuisPloegNavigation.Stadion)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Wedstrijd"); }
        }

        public async Task Add(Wedstrijd entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Wedstrijd");
            }
        }

        public async Task Update(Wedstrijd entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Wedstrijd");
            }
        }

        public async Task Delete(Wedstrijd entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Wedstrijd");
            }
        }
    }
}