using System;
using FluentValidation;

public class AcceptOrderValidator : AbstractValidator<AcceptOrderRequest>
{
	public AcceptOrderValidator()
	{
        RuleFor(order => order.SupplierId)
             .NotEmpty().WithMessage("Supplier ID is required.");
    }
}