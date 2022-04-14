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
    public class VakOmschrijvingDAO : IDAO<VakOmschrijving>
    {

        private readonly ProLeagueDbContext _dbContext;

        public VakOmschrijvingDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }
        public async Task<IEnumerable<VakOmschrijving>> GetAll()
        {
            try
            {
                return await _dbContext.VakOmschrijvings
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO VakOmschrijving");
            }
        }

        public async Task<VakOmschrijving> FindById(int id)
        {
            try
            {
                return await _dbContext.VakOmschrijvings
                         .Where(s => s.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO VakOmschrijving"); }
        }

        public async Task Add(VakOmschrijving entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO VakOmschrijving");
            }
        }

        public async Task Update(VakOmschrijving entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO VakOmschrijving");
            }
        }

        public async Task Delete(VakOmschrijving entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO VakOmschrijving");
            }
        }
    }
}