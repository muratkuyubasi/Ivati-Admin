using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateFamilyMemberCommand : IRequest<ServiceResponse<UserInformationDTO>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDay { get; set; }
        public int GenderId { get; set; }
        public string Nationality { get; set; }
        public bool isDualNationality { get; set; }
        public int? MemberTypeId { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string PlaceOfDeath { get; set; }
        public string BurialPlace { get; set; }
    }
}
