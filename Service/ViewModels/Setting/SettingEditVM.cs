using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Setting
{
    public class SettingEditVM
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public IFormFile File { get; set; }
        public string ExistFile { get; set; }
    }
}
