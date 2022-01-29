using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWithUpload.Models
{
    public class Image
    {
        [Required(ErrorMessage = "The Image Id is Required and can container alpha-numeric and _")]
        [RegularExpression(@"[a-zA-Z0-9_]+", ErrorMessage = "Must contain alpha-numeric and _")]
        public String Id {get; set; }
        [Required(ErrorMessage = "The Caption is Required!!")]
        [StringLength(40)]
        public String Caption {get; set;}
        [Required(ErrorMessage = "The Description is Required!!")]
        [StringLength(200)]
        public String Description { get; set; }
        [Required(ErrorMessage = "Date is Required and must be in mm/dd/yyyy format")]
        [DataType(DataType.Date)]
        public DateTime DateTaken { get; set; }
        public String Userid { get; set; }

        public Image()
        {
        }
    }
}