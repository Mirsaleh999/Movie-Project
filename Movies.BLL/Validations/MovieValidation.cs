using FluentValidation;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Validations
{
    public class MovieValidation: AbstractValidator<MovieCDto>
    {
        public MovieValidation() 
        {
            //Name
            RuleFor(d => d.Name).NotNull().WithMessage("The Name  area cannot be left empty.");
            RuleFor(d => d.Name).NotEmpty().WithMessage("The Name area cannot be left empty.");
            RuleFor(d => d.Name).MinimumLength(2).WithMessage("Please enter a minimum of 2 letters.");
            RuleFor(d => d.Name).MaximumLength(20).WithMessage("Please enter a maximum of 20 letters.");

            //Age
            RuleFor(d => d.Age).NotNull().WithMessage("The Age area cannot be left empty.");
            RuleFor(d => d.Age).NotEmpty().WithMessage("The Age area cannot be left empty.");
            RuleFor(d => d.Age.ToString()).MaximumLength(2).WithMessage("Please enter a maximum of 2 letters.");

            //Movie Time
            RuleFor(d => d.MovieTime).NotNull().WithMessage("The Movie Time area cannot be left empty.");
            RuleFor(d => d.MovieTime).NotEmpty().WithMessage("The Movie Time area cannot be left empty.");

            //Year
            RuleFor(d => d.Year).NotNull().WithMessage("The Year area cannot be left empty.");
            RuleFor(d => d.Year).NotEmpty().WithMessage("The Year area cannot be left empty.");
            RuleFor(d => d.Year.ToString()).MinimumLength(4).WithMessage("Please enter a minimum of 4 letters.");
            RuleFor(d => d.Year.ToString()).MaximumLength(4).WithMessage("Please enter a maximum of 4 letters.");

            //Description
            RuleFor(d => d.Description).NotNull().WithMessage("The Description area cannot be left empty.");
            RuleFor(d => d.Description).NotEmpty().WithMessage("The Description area cannot be left empty.");
            RuleFor(d => d.Description).MinimumLength(30).WithMessage("Please enter a minimum of 30 letters.");

            //Director
            RuleFor(d => d.Director).NotNull().WithMessage("The Director area cannot be left empty.");
            RuleFor(d => d.Director).NotEmpty().WithMessage("The Director area cannot be left empty.");
            RuleFor(d => d.Director).MinimumLength(3).WithMessage("Please enter a minimum of 3 letters.");
            RuleFor(d => d.Director).MaximumLength(18).WithMessage("Please enter a maximum of 18 letters.");

  
        }
    }
}
