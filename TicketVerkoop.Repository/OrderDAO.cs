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
    public class orderDAO : IDAO<Order>
    {
        private readonly ProLeagueDbContext _dbContext;

        public orderDAO()
        {
            _dbContext = new ProLeagueDbContext();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            try
            {
                return await _dbContext.Orders.Include(o => o.Client).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw new Exception("error DAO order");
            }
        }

        public async Task<Order> FindById(int id)
        {
            try
            {
                return await _dbContext.Orders
                         .Where(o => o.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO order"); }
        }

        public async Task Add(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO order");
            }
        }

        public async Task Update(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO order");
            }
        }

        public async Task Delete(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("error DAO order");
            }
        }
    }
}