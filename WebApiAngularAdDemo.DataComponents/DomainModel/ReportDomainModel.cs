using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAngularAdDemo.DataComponents.DomainModel
{
    public class ReportDomainModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public double MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
    }
}
