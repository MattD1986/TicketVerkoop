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
    public class AbonnementDAO : IDAO<Abonnement>
    {
        private readonly ProLeagueDbContext _dbContext;

        public AbonnementDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Abonnement>> GetAll()
        {
            try
            {
                return await _dbContext.Abonnements
                    .Include(a => a.Order)
                    .Include(a => a.Club)
                    .Include(a => a.Vak)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Abonnement");
            }
        }

        public async Task<Abonnement> FindById(int id)
        {
            try
            {
                 return await _dbContext.Abonnements
                         .Where(a => a.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Abonnement"); }
        }

        public async Task Add(Abonnement entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Abonnement");
            }
        }

        public async Task Update(Abonnement entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Abonnement");
            }
        }

        public async Task Delete(Abonnement entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Abonnement");
            }
        }
    }
}