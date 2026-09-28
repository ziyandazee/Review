using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Review.Models
{
	public class CustReview
	{
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string BusinessName { get; set; }

        [Required]
        
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        
    }
}
