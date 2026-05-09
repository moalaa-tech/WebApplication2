using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Contact;
using CRM.WebApp.ViewModels.Contact;

namespace CRM.WebApp.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<ContactDto, ContactViewModel>().ReverseMap();
        }
    }

}
