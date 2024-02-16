using MediatR;
using Microsoft.AspNetCore.Http;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.MediatR.Commands
{
    public class UploadPhotoCommand : IRequest<string>
    {
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }
        public int Width { get; set; } = 1600;
        public int Height { get; set; } = 900;
    }
}
