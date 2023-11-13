using System;
using FluentValidation;

public class AcceptRequestValidator : AbstractValidator<AcceptRequestRequest>
{
	public AcceptRequestValidator()
	{
        RuleFor(request => request.ContractId)
             .NotEmpty().WithMessage("Contract ID is required.");

        RuleFor(request => request.SupplierId)
             .NotEmpty().WithMessage("Supplier ID is required.");
    }
}