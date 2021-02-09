using System.ComponentModel.DataAnnotations.Schema;
using Arc.Core.Gameplay.Types;

namespace Arc.Database.Models.Characters
{
    [Table("avatar_look")]
    public class AvatarLook : IModel
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("skin")]
        public byte Skin { get; set; }

        [Column("face")]
        public int Face { get; set; }

        [Column("hair")]
        public int Hair { get; set; }

        [Column("facial_marks")]
        public int? FacialMarks { get; set; }

        [Column("ear_type")]
        public Ear EarType { get; set; }

    }
}
