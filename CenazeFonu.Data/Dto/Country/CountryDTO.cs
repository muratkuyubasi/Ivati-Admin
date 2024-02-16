﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Data.Dto
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Langcode { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<CityDTO> Cities { get; set; }
    }
}