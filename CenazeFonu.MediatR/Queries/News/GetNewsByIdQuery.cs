using CenazeFonu.Data.Dto;
using CenazeFonu.DataDto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries
{
    public class GetNewsByIdQuery  : IRequest<ServiceResponse<NewsDTO>>
    {
        public int Id { get; set; }
    }
}
