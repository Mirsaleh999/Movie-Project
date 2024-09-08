using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Dtos
{
    public class MoviesDocumentDto : BaseDto
    {
        public int MovieCId { get; set; }
        public string? DocumentUrl { get; set; }
    }
}
