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
    public class UpdateFamilyAddressCommand : IRequest<ServiceResponse<AddressDTO>>
    {
        public Guid FamilyId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string District { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
