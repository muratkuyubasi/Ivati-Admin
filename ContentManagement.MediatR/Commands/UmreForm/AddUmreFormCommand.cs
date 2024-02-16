using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddUmreFormCommand : IRequest<ServiceResponse<UmreFormDTO>>
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int ClosestAssociationId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PlaceOfBirth { get; set; }
        public string SwedenIdentificationNumber { get; set; }
        public string TurkeyIdentificationNumber { get; set; }
        public int GenderId { get; set; }
        public int MaritalStatusId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportGivenDate { get; set; }
        public string PassportGivenPlace { get; set; }
        public DateTime PassportExpirationDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartureAirportId { get; set; }
        public int LandingAirportId { get; set; }
        public string Explanation { get; set; }
        public string? PassportPicture { get; set; }
        public string? HeadshotPicture { get; set; }
    }
}
