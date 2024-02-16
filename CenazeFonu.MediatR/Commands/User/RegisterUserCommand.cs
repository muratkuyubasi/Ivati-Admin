﻿using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;
using CenazeFonu.Helper;
using System;

namespace CenazeFonu.MediatR.Commands
{
    public class RegisterUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public bool IsDisabled { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public List<UserAllowedIPDto> UserAllowedIPs { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();

    }
}