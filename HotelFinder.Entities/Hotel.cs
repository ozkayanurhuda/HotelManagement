using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//dbdeki tablolarıma karşılık gelen csharp classlarından oluşur
namespace HotelFinder.Entities
{
    public class Hotel
    {
        //primary key ve bir bir artan id (1den başlar)
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }
    }
}
