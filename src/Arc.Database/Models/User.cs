using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Database.Models.Characters;

namespace Arc.Database.Models
{
    [Table("users")]
    public class User : IModel
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("world_id")]
        public byte WorldID { get; set; }

        [Column("character_slots")]
        public int CharacterSlots { get; set; }

        [Column("nx_credit")]
        public int NxCredit { get; set; }

        [Column("maple_points")]
        public int MaplePoints { get; set; }

        [ForeignKey("user_id")]
        public List<Character> Characters { get; set; }

        public User()
        {
            Characters = new List<Character>();
        }

    }
}
