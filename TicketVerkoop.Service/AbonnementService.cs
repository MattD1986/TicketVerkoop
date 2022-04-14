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
    public class AbonnementService : IService<Abonnement>
    {

        private IDAO<Abonnement> _abonnementDAO;

        public AbonnementService(IDAO<Abonnement> abonnementDAO)
        {
            _abonnementDAO = abonnementDAO;
        }


        public async Task<IEnumerable<Abonnement>> GetAll()
        {
            return await _abonnementDAO.GetAll();
        }


        public async Task Add(Abonnement entity)
        {
            await _abonnementDAO.Add(entity);
        }

        public async Task Delete(Abonnement entity)
        {
            await _abonnementDAO.Delete(entity);
        }

        public async Task<Abonnement> FindById(int Id)
        {
            return await _abonnementDAO.FindById(Id);
        }



        public async Task Update(Abonnement entity)
        {
            await _abonnementDAO.Update(entity);
        }


    }
}
