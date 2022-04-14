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
    public class CompetitieDAO : IDAO<Competitie>
    {
        private readonly ProLeagueDbContext _dbContext;

        public CompetitieDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Competitie>> GetAll()
        {
            try
            {
                return await _dbContext.Competities
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO Competitie");
            }
        }

        public async Task<Competitie> FindById(int id)
        {
            try
            {
                 return await _dbContext.Competities
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO Competitie"); }
        }

        public async Task Add(Competitie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Competitie");
            }
        }

        public async Task Update(Competitie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Competitie");
            }
        }

        public async Task Delete(Competitie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO Competitie");
            }
        }
    }
}