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
    public class PlaatsService : IService<Plaat>
    {

        private IDAO<Plaat> _plaatsDAO;

        public PlaatsService(IDAO<Plaat> plaatsDAO)
        {
            _plaatsDAO = plaatsDAO;
        }


        public async Task<IEnumerable<Plaat>> GetAll()
        {
            return await _plaatsDAO.GetAll();
        }


        public async Task Add(Plaat entity)
        {
            await _plaatsDAO.Add(entity);
        }

        public async Task Delete(Plaat entity)
        {
            await _plaatsDAO.Delete(entity);
        }

        public async Task<Plaat> FindById(int Id)
        {
            return await _plaatsDAO.FindById(Id);
        }



        public async Task Update(Plaat entity)
        {
            await _plaatsDAO.Update(entity);
        }


    }
}
