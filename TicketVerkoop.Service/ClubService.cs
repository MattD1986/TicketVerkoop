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
    public class ClubService : IService<Club>
    {

        private IDAO<Club> _clubDAO;

        public ClubService(IDAO<Club> clubDAO)
        {
            _clubDAO = clubDAO;
        }


        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _clubDAO.GetAll();
        }


        public async Task Add(Club entity)
        {
            await _clubDAO.Add(entity);
        }

        public async Task Delete(Club entity)
        {
            await _clubDAO.Delete(entity);
        }

        public async Task<Club> FindById(int Id)
        {
            return await _clubDAO.FindById(Id);
        }



        public async Task Update(Club entity)
        {
            await _clubDAO.Update(entity);
        }


    }
}
