using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class ExecutiveDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int? CityId { get; set; }
    }

    public class ExecutiveSingleUserDTO
    {
        public Guid Id { get; set; }

        public int? FamilyId { get; set; }

        public string? PicturePath { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string CreationDate { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public Guid? RoleId { get; set; }

        public int? CityId { get; set; }
        public string? CityName { get; set; }
    }

    public class ExecutiveUserDTO
    {
        public Guid Id { get; set; }

        public int? FamilyId { get; set; }

        public string? PicturePath { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string CreationDate { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
        public Guid? RoleId { get; set; }

        public ICollection<CitySimpleDTO> Cities { get; set; }
    }

    public class ExecutivePaginationDto
    {
        public List<ExecutiveUserDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
