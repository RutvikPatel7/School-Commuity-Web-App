using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class CommunityMembership
    {

        public int ID { get; set; }
        public int StudentID { get; set; }

        public string CommunityID { get; set; }

        public Student Student { get; set; }

        public Community Community { get; set; }

        
    }
}
