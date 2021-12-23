using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Models.Entities
{
    public class Deneme
    {
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Ad { get; set; }

    }
}
