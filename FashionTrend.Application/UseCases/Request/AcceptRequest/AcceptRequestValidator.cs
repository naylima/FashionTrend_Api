using System;
using FluentValidation;

public class AcceptRequestValidator : AbstractValidator<AcceptRequestRequest>
{
	public AcceptRequestValidator()
	{
        RuleFor(request => request.SupplierId)
             .NotEmpty().WithMessage("Supplier ID is required.");
    }
}