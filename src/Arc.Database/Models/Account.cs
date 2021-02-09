using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Database.Models
{
    [Table("accounts")]
    public class Account : IModel
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("pic")]
        [MaxLength(25)] // TODO: Check max length of the pic and adjust max length accordingly
        public string Pic { get; set; }

        [Column(("creation_date"), TypeName = "DATETIME")]
        public DateTime CreationDate { get; set; }

        [ForeignKey("acc_id")]
        public HashSet<User> Users { get; set; }

        public Account()
        {
            Users = new HashSet<User>();
        }
    }
}
