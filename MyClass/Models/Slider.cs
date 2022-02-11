namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Img { get; set; }

        public int Orders { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
