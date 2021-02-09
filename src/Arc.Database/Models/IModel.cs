using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models
{
    public interface IModel
    {
        [Column("id")]
        public int ID { get; set; }
    }
}
