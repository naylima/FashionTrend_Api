using System;
using FluentValidation;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
	public CreateOrderValidator()
	{
        RuleFor(order => order.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(order => order.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}