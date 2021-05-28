using System.ComponentModel.DataAnnotations.Schema;

namespace Tesis.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public int Url { get; set; }
        public int IsMain { get; set; }
        public int PublicId { get; set; }
    }
}