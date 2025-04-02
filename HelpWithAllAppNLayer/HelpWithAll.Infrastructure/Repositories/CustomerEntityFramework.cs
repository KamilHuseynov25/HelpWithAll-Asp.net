using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Repositories.Base;
using HelpWithAll.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpWithAll.Infrastructure.Repositories
{
    public class CustomerEntityFrameworkRepository : ICustomerRepository
    {
        private readonly HelperDbContext _context;

        public CustomerEntityFrameworkRepository(HelperDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Customer>> SelectAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> SelectCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> UpdateCustomerAsync(Customer updatedCustomer)
        {
            _context.Customers.Update(updatedCustomer);
            return await _context.SaveChangesAsync() > 0;
        }

    

    }
}