using System;
using FluentValidation;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentRequest>
{
	public CreatePaymentValidator()
	{
        RuleFor(p => p.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(p => p.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        RuleFor(p => p.PaymentDate)
            .NotEmpty().WithMessage("Payment date is required.")
            .LessThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Payment date cannot be in the future.");

        RuleFor(p => p.ContractId)
            .NotEmpty().WithMessage("Contract ID is required.");
    }
}