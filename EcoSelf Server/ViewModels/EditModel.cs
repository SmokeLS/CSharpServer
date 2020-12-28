using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcoSelf_Server.ViewModels
{
    public class EditModel
    {
        [Required(ErrorMessage = "Не указан id")]
        public int Id { get; set; }

        public string BarCode { get; set; }
        public string Name { get; set; }
        public string RecycMatr { get; set; }
        public string Desc { get; set; }

    }
}
