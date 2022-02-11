namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Type { get; set; }

        public int TableId { get; set; }

        public int Orders { get; set; }

        [Required]
        public string Position { get; set; }

        public int ParentId { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
