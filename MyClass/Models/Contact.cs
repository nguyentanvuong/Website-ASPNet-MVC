namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Detail { get; set; }

        public string ReplayDetail { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
