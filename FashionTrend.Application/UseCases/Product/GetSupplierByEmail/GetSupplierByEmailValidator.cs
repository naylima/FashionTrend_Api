using System;
using FluentValidation;

public class GetSupplierByEmailValidator : AbstractValidator<GetSupplierByEmailRequest>
{
	public GetSupplierByEmailValidator()
	{
        RuleFor(s => s.Email).NotEmpty().WithMessage("Email is required.");
    }
}

