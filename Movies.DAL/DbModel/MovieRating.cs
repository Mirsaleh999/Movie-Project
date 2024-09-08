using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserIp { get; set; }
        public double Rating { get; set; }
        public MovieC Movie { get; set; }
    }
}
