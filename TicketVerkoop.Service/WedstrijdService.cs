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
    public class WedstrijdService : IService<Wedstrijd>
    {

        private IDAO<Wedstrijd> _wedstrijdDAO;

        public WedstrijdService(IDAO<Wedstrijd> wedstrijdDAO)
        {
            _wedstrijdDAO = wedstrijdDAO;
        }


        public async Task<IEnumerable<Wedstrijd>> GetAll()
        {
            return await _wedstrijdDAO.GetAll();
        }


        public async Task Add(Wedstrijd entity)
        {
            await _wedstrijdDAO.Add(entity);
        }

        public async Task Delete(Wedstrijd entity)
        {
            await _wedstrijdDAO.Delete(entity);
        }

        public async Task<Wedstrijd> FindById(int Id)
        {
            return await _wedstrijdDAO.FindById(Id);
        }



        public async Task Update(Wedstrijd entity)
        {
            await _wedstrijdDAO.Update(entity);
        }


    }
}
