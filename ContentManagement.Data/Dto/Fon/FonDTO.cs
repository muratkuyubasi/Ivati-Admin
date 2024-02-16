using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class FonDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public string BirthPlace { get; set; }

        public string Gender { get; set; }

        public string Nationalitiy { get; set; }

        public Guid SpouseId { get; set; }

        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public string SpouseIdenticifationNumber { get; set; }

        public string SpouseBirthPlace { get; set; }

        public string SpouseGender { get; set; }

        public string SpouseNationalitiy { get; set; }

        public string Street { get; set; }

        public string PostCode { get; set; }

        public string District { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }


        public Guid ChildId { get; set; }

        public string ChildFullName { get; set; }
        public string ChildIdentificationNumber { get; set; }
        public string ChildBirthPlace { get; set; }

        public string Closeness { get; set; }

        public DateTime ChildBirthDate { get; set; }
    }
}
