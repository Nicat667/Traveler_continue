﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels
{
    public class EmailSubscribeVM
    {
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
