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
    public class StadionService : IService<Stadion>
    {

        private IDAO<Stadion> _stadionDAO;

        public StadionService(IDAO<Stadion> stadionDAO)
        {
            _stadionDAO = stadionDAO;
        }


        public async Task<IEnumerable<Stadion>> GetAll()
        {
            return await _stadionDAO.GetAll();
        }


        public async Task Add(Stadion entity)
        {
            await _stadionDAO.Add(entity);
        }

        public async Task Delete(Stadion entity)
        {
            await _stadionDAO.Delete(entity);
        }

        public async Task<Stadion> FindById(int Id)
        {
            return await _stadionDAO.FindById(Id);
        }



        public async Task Update(Stadion entity)
        {
            await _stadionDAO.Update(entity);
        }


    }
}
