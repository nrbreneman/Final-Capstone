
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication.Web.Models
{
    public class League
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("League Name")]
        public string LeagueName { get; set; }

        [Required]
        [DisplayName("Organization")]
        public string Org { get; set; }

        [Required]
        public string Sport { get; set; }
    }
}