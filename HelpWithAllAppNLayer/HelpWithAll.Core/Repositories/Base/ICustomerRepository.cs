using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;

namespace HelpWithAll.Core.Repositories.Base;
public interface ICustomerRepository
{
    public Task<bool> InsertCustomerAsync(Customer customer);

    public Task<IEnumerable<Customer>> SelectAllCustomersAsync();

    public Task<Customer> SelectCustomerByIdAsync(int id);
    public Task<bool> DeleteCustomerAsync(int id);
    public Task<bool> UpdateCustomerAsync(Customer customer);
}
