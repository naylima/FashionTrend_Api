using System;
using FluentValidation;

namespace FashionTrend.Application.UseCases.Supplier.CreateSupplier;

public class AddMaterialToSupplierValidator : AbstractValidator<AddMaterialToSupplierRequest>
{
	public AddMaterialToSupplierValidator()
	{
        RuleFor(s => s.MaterialIds)
            .NotEmpty().WithMessage("At least one Material ID is required.");

        RuleFor(s => s.SupplierId)
            .NotEmpty().WithMessage("Supplier ID is required.");
    }
}