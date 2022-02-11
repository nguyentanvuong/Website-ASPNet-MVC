namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string Email { get; set; }

        public int Gender { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Img { get; set; }

        public int Access { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Created_at { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
