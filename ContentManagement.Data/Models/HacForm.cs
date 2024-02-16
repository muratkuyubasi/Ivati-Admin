using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Models
{
    public class HacForm
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int ClosestAssociationId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PlaceOfBirth { get; set; }
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
        public string? Explanation { get; set; }
        public string? PassportPicture { get; set; }
        public string? HeadshotPicture { get; set; }

        public int? PeriodId { get; set; }

        public virtual Association ClosestAssociation { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual RoomType RoomType { get; set; }

        public virtual Airport DepartureAirport { get; set; }

        public virtual Airport LandingAirport { get; set; }

        public virtual HacPeriod Period { get; set; }
    }
}
