using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using BlazorNet8.Shared.Models;
using System.Net.Http.Json;
using System.Configuration;
using Microsoft.Extensions.Configuration;


namespace BlazorNet8.Shared.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public EmployeeService(IConfiguration config, 
            HttpClient client)
        {
            _config = config;
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            string baseUri = _config.GetSection("BaseUri").Value!;
            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUri);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _client.GetFromJsonAsync<List<Employee>>("/employees");
            return employees;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            HttpResponseMessage result = await _client
                .PostAsJsonAsync("/employees/create", employee);
            return await result.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            HttpResponseMessage result = await _client
                .PutAsJsonAsync("/employees/update", employee);
        }

        public async Task DeleteEmployee(int Id)
        {
            await _client.DeleteAsync($"employees/delete/{Id}");
        }
    }
}
