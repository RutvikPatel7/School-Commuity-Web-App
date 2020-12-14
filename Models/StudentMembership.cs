using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Models
{
    public class StudentMembership 
    {
        public int ID { get; set; }
       
        public int StudentID { get; set; }
        public string CommunityID { get; set; }

        public Student Student { get; set; }

        public Community Community { get; set; }
    }
}
