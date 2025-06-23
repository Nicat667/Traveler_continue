﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
