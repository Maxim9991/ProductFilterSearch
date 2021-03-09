using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogForm.Entities
{
    [Table("tblFilterNames")]
    public class FilterName
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(250)]
        public string Name { get; set; }
        public virtual ICollection<FilterNameGroup> FilterNameGroups { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }

    }
}
