//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUP_with_Image_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string image_path { get; set; }

        public HttpPostedFileBase Imagefile { get; set; }
    }
}
