using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.MediatR.Command;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class SetWifesIdentificationNumberCommandHandler : IRequestHandler<SetWifesIdentificationNumberCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyRepository _familyRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        public SetWifesIdentificationNumberCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> unitOfWork, IFamilyRepository familyRepository, IFamilyMemberRepository familyMemberRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = unitOfWork;
            _familyRepository = familyRepository;
            _familyMemberRepository = familyMemberRepository;
        }
        public async Task<bool> Handle(SetWifesIdentificationNumberCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = new CenazeFonuUyeleriContext())
            {
                foreach (var k in dbcontext.CenazeFonuUyeleris)
                {
                    if (k.Spouse == null)
                    {
                        var b = _userRepository.All.AsNoTracking().Include(X => X.Family.FamilyMembers).ThenInclude(X => X.MemberUser).FirstOrDefault(x => x.IdentificationNumber == k.IdentificationNumber);
                        if (b.Family != null)
                        {
                            var spouseExists = b.Family.FamilyMembers.FirstOrDefault(X => X.MemberTypeId == 2);
                            if (spouseExists != null)
                            {
                                _uow.Context.ChangeTracker.Clear();
                                _familyMemberRepository.Remove(spouseExists);
                                _userRepository.Remove(spouseExists.MemberUser);
                            }
                        }
                    }
                    _uow.Context.SaveChanges();
                    #region Eş Kimlik Numarası Atama
                    //if (b != null)
                    //{
                    //    if (b.Family != null)
                    //    {
                    //        var wife = b.Family.FamilyMembers.FirstOrDefault(x => x.MemberTypeId == 2);
                    //        if (wife != null)
                    //        {
                    //            wife.MemberUser.IdentificationNumber = k.SpouseIdentificationNumber;
                    //            _uow.Context.ChangeTracker.Clear();
                    //            _userRepository.Update(wife.MemberUser);
                    //        }
                    //    }
                    //}
                    #endregion
                    #region eski yol
                    //foreach (var family in families)
                    //{

                    //    var wife = family.FamilyMembers.FirstOrDefault(X => X.MemberTypeId == 2);
                    //    wife.MemberUser.IdentificationNumber = k.SpouseIdentificationNumber;
                    //    _userRepository.Update(wife.MemberUser);
                    //    _uow.Context.SaveChanges();
                    //}
                    #endregion
                }
            }
            _uow.Context.Dispose();
            return true;
        }
    }
}
