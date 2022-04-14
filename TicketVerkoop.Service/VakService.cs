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
    public class VakService : IService<Vak>
    {

        private IDAO<Vak> _vakkenDAO;

        public VakService(IDAO<Vak> vakkenDAO)
        {
            _vakkenDAO = vakkenDAO;
        }


        public async Task<IEnumerable<Vak>> GetAll()
        {
            return await _vakkenDAO.GetAll();
        }


        public async Task Add(Vak entity)
        {
            await _vakkenDAO.Add(entity);
        }

        public async Task Delete(Vak entity)
        {
            await _vakkenDAO.Delete(entity);
        }

        public async Task<Vak> FindById(int Id)
        {
            return await _vakkenDAO.FindById(Id);
        }



        public async Task Update(Vak entity)
        {
            await _vakkenDAO.Update(entity);
        }


    }
}
