using FluentValidation;
using BelkiHakiki.Core.DTOs;

namespace BelkiHakiki.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {

            RuleFor(x => x.Barcode).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
            RuleFor(x => x.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
            
        }


    }
}
