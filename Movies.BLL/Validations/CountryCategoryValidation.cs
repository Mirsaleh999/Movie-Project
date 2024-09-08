using FluentValidation;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Validations
{
    public class CountryCategoryValidation : AbstractValidator<CountryCategoryDto>
    {
        public CountryCategoryValidation()
        {
            RuleFor(d => d.Name).NotNull().WithMessage("The name area cannot be left empty.");
            RuleFor(d => d.Name).NotEmpty().WithMessage("The name area cannot be left empty.");
            RuleFor(d => d.Name).MinimumLength(3).WithMessage("Please enter a minimum of 3 letters.");
            RuleFor(d => d.Name).MaximumLength(16).WithMessage("Please enter a maximum of 16 letters.");

        }

    }
}