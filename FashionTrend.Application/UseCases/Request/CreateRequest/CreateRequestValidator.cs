using System;
using FluentValidation;

public class CreateRequestValidator : AbstractValidator<CreateRequestRequest>
{
	public CreateRequestValidator()
	{
        RuleFor(request => request.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(request => request.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}