using AutoMapper;
using MotoShop.Models.DTO;
using MotoShop.Models.Requests;
using MotoShop.Models.Responses;

namespace MotoShop.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Moto, MotoResponse>().ReverseMap();
            CreateMap<Client, ClientResponse>().ReverseMap();
            CreateMap<Part, PartResponse>().ReverseMap();
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
            CreateMap<Service, ServiceResponse>().ReverseMap();

            CreateMap<MotoRequest, Moto>().ReverseMap();
            CreateMap<ClientRequest, Client>().ReverseMap();
            CreateMap<PartRequest, Part>().ReverseMap();
            CreateMap<EmployeeRequest, Employee>().ReverseMap();
            CreateMap<ServiceRequest, Service>().ReverseMap();

            CreateMap<MotoUpdateRequest, Moto>().ReverseMap();
            CreateMap<ClientUpdateRequest, Client>().ReverseMap();
            CreateMap<PartUpdateRequest, Part>().ReverseMap();
            CreateMap<EmployeeUpdateRequest, Employee>().ReverseMap();
            CreateMap<ServiceUpdateRequest, Service>().ReverseMap();
        }
    }
}
