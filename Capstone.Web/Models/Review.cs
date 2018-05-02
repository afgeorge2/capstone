using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Review
    {
        public int Rating { get; set; }
        public string ReviewPost { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public int AverageReview { get; set; }

        //get count of reviews and sum of ratings
        //plug in to GetAverageReview method


        //private int GetAverageReview()
        //{

        //}
    }
}