using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcoSelf_Server.ViewModels
{
    public class DeleteModel
    {
        [Required(ErrorMessage = "Не указан id")]
        public int Id { get; set; }
    }
}
