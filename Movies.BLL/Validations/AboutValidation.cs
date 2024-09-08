using FluentValidation;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Validations
{
    public class AboutValidation: AbstractValidator<AboutDto>
    {
        public AboutValidation() 
        {
            RuleFor(d => d.Position).NotNull().WithMessage("The Position area cannot be left empty.");
            RuleFor(d => d.Position).NotEmpty().WithMessage("The Position area cannot be left empty.");
            RuleFor(d => d.Description).NotNull().WithMessage("The Description area cannot be left empty.");
            RuleFor(d => d.Description).NotEmpty().WithMessage("The Description area cannot be left empty.");
            RuleFor(d => d.Position).MinimumLength(5).WithMessage("Please enter a minimum of 3 letters.");
            RuleFor(d => d.Description).MinimumLength(30).WithMessage("Please enter a minimum of 3 letters.");
            RuleFor(d => d.Position).MaximumLength(35).WithMessage("Please enter a maximum of 16 letters.");
        }
    }
}
