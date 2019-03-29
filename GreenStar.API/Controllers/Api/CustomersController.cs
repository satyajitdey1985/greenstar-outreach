using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using GreenStar.API.Dtos;
using GreenStar.API.Models;
using GreenStar.API.Core;

namespace GreenStar.API.Controllers.Api
{
    public class CustomersController : ApiController
    {       
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
        }

        //GET api/customers
        public IHttpActionResult GetCustomers()
        {
            var customers = _unitOfWork.Customers.GetCustomersWithMembershipType();

            var customerDtos = customers.Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET api/customers/id  (Get customer without MembershipType)
        public IHttpActionResult GetCustomer(int id)
        {
            var customerInDb = _unitOfWork.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();
            
            return Ok(Mapper.Map<Customer, CustomerDto>(customerInDb));
        }

        //POST api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT api/customers/id
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _unitOfWork.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _unitOfWork.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _unitOfWork.Customers.Remove(customerInDb);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}

