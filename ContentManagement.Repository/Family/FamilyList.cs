using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain.Migrations;

namespace ContentManagement.Repository
{
    public class FamilyList : List<FamilyDTO>
    {
        public FamilyList()
        {
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public FamilyList(List<FamilyDTO> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<int> GetCount(IQueryable<Family> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<FamilyList> Create(IQueryable<Family> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new FamilyList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<List<FamilyDTO>> GetDtos(IQueryable<Family> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(c => new FamilyDTO
                {
                    Id = c.Id,
                    //Address = (Data.Models.Address)c.Address,
                    //FamilyMembers = (ICollection<FamilyMemberDTO>)c.FamilyMembers,
                    //Debtors = (List<DebtorDTO>)c.Debtors,
                    FamilyNotes = (ICollection<FamilyNoteDTO>)c.FamilyNotes,
                    IsActive = c.IsActive,
                    MemberId = c.MemberId,
                    Name = c.Name,
                    ReferenceNumber = c.ReferenceNumber,
                    IsDeleted = c.IsDeleted,
                    UserId = c.UserId,
                })
                .ToListAsync();
            return entities;
        }
    }
}
