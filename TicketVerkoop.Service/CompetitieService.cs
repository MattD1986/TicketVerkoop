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
    public class CompetitieService : IService<Competitie>
    {

        private IDAO<Competitie> _CompetitieDAO;

        public CompetitieService(IDAO<Competitie> competitieDAO)
        {
            _CompetitieDAO = competitieDAO;
        }


        public async Task<IEnumerable<Competitie>> GetAll()
        {
            return await _CompetitieDAO.GetAll();
        }


        public async Task Add(Competitie entity)
        {
            await _CompetitieDAO.Add(entity);
        }

        public async Task Delete(Competitie entity)
        {
            await _CompetitieDAO.Delete(entity);
        }

        public async Task<Competitie> FindById(int Id)
        {
            return await _CompetitieDAO.FindById(Id);
        }



        public async Task Update(Competitie entity)
        {
            await _CompetitieDAO.Update(entity);
        }


    }
}
