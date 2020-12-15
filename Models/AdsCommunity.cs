using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Models
{
    public class AdsCommunity 
    {
        public int ID { get; set; }

        public int AdvertisementID { get; set; }

        public Advertisement Advertisement { get; set; }

        public string CommunityID { get; set; }

        public Community Community { get; set; }
    }
}
