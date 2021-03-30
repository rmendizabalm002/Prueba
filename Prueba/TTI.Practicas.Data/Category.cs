using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTI.Practicas.Data
{
    [Table("Categories")]
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
