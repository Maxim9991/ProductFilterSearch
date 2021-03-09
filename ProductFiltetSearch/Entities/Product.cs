using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogForm.Entities
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal? Price { get; set; }


        [StringLength(maximumLength: 250)]
        public string Image { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string UniqueName { get; set; }

        public virtual ICollection<Filter> Filters { get; set; }


    }
}
