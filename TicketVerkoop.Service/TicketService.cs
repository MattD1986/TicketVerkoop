using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Repository.Interfaces;
using TicketVerkoop.Service.Interfaces;

namespace TicketVerkoop.Service
{
    public class TicketService : IService<Ticket>
    {

        private IDAO<Ticket> _ticketDAO;

        public TicketService(IDAO<Ticket> ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }


        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _ticketDAO.GetAll();
        }


        public async Task Add(Ticket entity)
        {
            await _ticketDAO.Add(entity);
        }

        public async Task Delete(Ticket entity)
        {
            await _ticketDAO.Delete(entity);
        }

        public async Task<Ticket> FindById(int Id)
        {
            return await _ticketDAO.FindById(Id);
        }



        public async Task Update(Ticket entity)
        {
            await _ticketDAO.Update(entity);
        }


    }
}
