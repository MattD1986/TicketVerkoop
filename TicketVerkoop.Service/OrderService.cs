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
    public class OrderService : IService<Order>
    {

        private IDAO<Order> _orderDAO;

        public OrderService(IDAO<Order> orderDAO)
        {
            _orderDAO = orderDAO;
        }


        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderDAO.GetAll();
        }


        public async Task Add(Order entity)
        {
            await _orderDAO.Add(entity);
        }

        public async Task Delete(Order entity)
        {
            await _orderDAO.Delete(entity);
        }

        public async Task<Order> FindById(int Id)
        {
            return await _orderDAO.FindById(Id);
        }



        public async Task Update(Order entity)
        {
            await _orderDAO.Update(entity);
        }


    }
}
