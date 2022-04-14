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
    public class VakOmschrijvingService : IService<VakOmschrijving>
    {

        private IDAO<VakOmschrijving> _VakOmschrijvingDAO;

        public VakOmschrijvingService(IDAO<VakOmschrijving> VakOmschrijvingDAO)
        {
            _VakOmschrijvingDAO = VakOmschrijvingDAO;
        }


        public async Task<IEnumerable<VakOmschrijving>> GetAll()
        {
            return await _VakOmschrijvingDAO.GetAll();
        }


        public async Task Add(VakOmschrijving entity)
        {
            await _VakOmschrijvingDAO.Add(entity);
        }

        public async Task Delete(VakOmschrijving entity)
        {
            await _VakOmschrijvingDAO.Delete(entity);
        }

        public async Task<VakOmschrijving> FindById(int Id)
        {
            return await _VakOmschrijvingDAO.FindById(Id);
        }



        public async Task Update(VakOmschrijving entity)
        {
            await _VakOmschrijvingDAO.Update(entity);
        }


    }
}
