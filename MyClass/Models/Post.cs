namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int Id { get; set; }

        public int TopId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Detail { get; set; }

        [Required]
        public string Img { get; set; }

        [Required]
        public string Type { get; set; }

        public string Metakey { get; set; }

        public string Metadesc { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
