using System;
using FluentValidation;

namespace FashionTrend.Application.UseCases.Supplier.CreateSupplier;

public class CreateMaterialValidator : AbstractValidator<CreateMaterialRequest>
{
	public CreateMaterialValidator()
	{
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(m => m.Color)
            .NotEmpty().WithMessage("Color is required.")
            .MinimumLength(3).WithMessage("Color must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Color must not exceed 20 characters.");
    }
}