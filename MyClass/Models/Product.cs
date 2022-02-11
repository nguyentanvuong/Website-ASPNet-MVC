namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        public int CatId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        public int TypeId { get; set; }

        [Required]
        public string Img { get; set; }

        [Required]
        public string Detail { get; set; }

        public int Number { get; set; }

        public float Price { get; set; }

        public float PriceSale { get; set; }

        public string Metakey { get; set; }

        public string Metadesc { get; set; }

        public DateTime? TopHot { get; set; }

        public DateTime? TopNew { get; set; }

        public DateTime? Featured { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
