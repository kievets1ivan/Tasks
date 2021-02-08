using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Exceptions;
using Tasks.DAL.Repositories;

namespace Tasks.BLL.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployeeById(int employeeId);
        Task<IEnumerable<EmployeeDTO>> GetAll();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
                               IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeRepository.GetById(employeeId);

            if (employee == null)
                throw new EmployeeNotFoundException();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll() => _mapper.Map<IEnumerable<EmployeeDTO>>(await _employeeRepository.GetAll());

    }
}
