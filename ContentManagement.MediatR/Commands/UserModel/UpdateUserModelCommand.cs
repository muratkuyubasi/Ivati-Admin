using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;
using ContentManagement.Data.Models;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateUserModelCommand : IRequest<ServiceResponse<UserModelDTO>>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }

        public string BirthPlace { get; set; }
        public DateTime? BirthDay { get; set; }
        public int GenderId { get; set; }
        public string Nationality { get; set; }
        public ICollection<UserModelDTO> FamilyMembers { get; set; } = null;
        public virtual AddressDTO Address { get; set; }
    }
}
