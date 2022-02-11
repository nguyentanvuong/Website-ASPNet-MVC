namespace MyClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        [Required]
        public string code { get; set; }

        public int UserId { get; set; }

        public DateTime? CreatedDate {get; set;}

        [Required]
        public string Deliveryaddress { get; set; }

        [Required]
        public string Deliveryname { get; set; }

        [Required]
        public string Deliveryphone { get; set; }

        [Required]
        public string Deliveryemail { get; set; }

        public int? Updated_by { get; set; }

        public DateTime? Updated_at { get; set; }

        public int Status { get; set; }
    }
}
