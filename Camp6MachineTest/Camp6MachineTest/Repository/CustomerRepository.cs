using Camp6MachineTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Camp6MachineTest.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LoanMS_dbContext _Context;

        public CustomerRepository(LoanMS_dbContext context)
        {
            _Context = context;
        }

        #region Get all Customer
        public async Task<List<CustomerTbl>> GetAllCustomerTbl()
        {
            if (_Context != null)
            {
                return await _Context.CustomerTbl.ToListAsync();
            }
            return null;
        }
        #endregion

        #region add a customer
        public async Task<int> AddCustomer(CustomerTbl customer)
        {
            if (_Context != null)
            {
                await _Context.CustomerTbl.AddAsync(customer);
                await _Context.SaveChangesAsync();  // commit the transction
                return customer.CId;
            }
            return 0;
        }
        #endregion
        #region Update Customer
        public async Task UpdateCustomer(CustomerTbl customer)
        {
            if (_Context != null)
            {
                _Context.Entry(customer).State = EntityState.Modified;
                _Context.CustomerTbl.Update(customer);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetCustomerById
        public async Task<CustomerTbl> GetCustomerById(int? id)
        {
            if (_Context != null)
            {
                var customer = await _Context.CustomerTbl.FindAsync(id);   //primary key
                return customer;
            }
            return null;
        }
        #endregion

        #region Delete Customer
        public async Task<int> DeleteCustomer(int? id)
        {
            if (_Context != null)
            {
                var employee = await (_Context.CustomerTbl.FirstOrDefaultAsync(emp => emp.CId == id));

                if (employee != null)
                {
                    //Delete
                    _Context.CustomerTbl.Remove(employee);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return employee.CId;
                }
            }
            return 0;
        }
        #endregion
    }
}
