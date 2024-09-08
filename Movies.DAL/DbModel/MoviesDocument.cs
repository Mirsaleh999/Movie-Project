using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    public class MoviesDocument:BaseEntity
    {
        public int MovieCId { get; set; }
        public MovieC MovieC { get; set; }
        public string DocumentUrl { get; set; }

    }
}
