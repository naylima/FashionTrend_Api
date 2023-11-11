using System;
using FluentValidation;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
	public UpdateProductValidator()
	{
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(50).WithMessage("Email must not exceed 255 characters.");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Price cannot be empty")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value");
    }
}

