using BlazorNet8.Shared.Data;
using BlazorNet8.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8.Shared.Services
{
    public class EmployeeRepository
    {
        public readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAll()
        {
            //await Task.Delay(1000);
            var employees = await _dbContext.Employee.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);
            return employee;
        }

        public async Task Create(Employee employee)
        {
            await _dbContext.AddAsync<Employee>(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Employee employee)
        {
            _dbContext.Update<Employee>(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);
            _dbContext.Employee.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
