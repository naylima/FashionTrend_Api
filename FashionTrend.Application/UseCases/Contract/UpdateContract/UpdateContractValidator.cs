using System;
using FluentValidation;

public class UpdateContractValidator : AbstractValidator<UpdateContractRequest>
{
	public UpdateContractValidator()
	{
        RuleFor(c => c.ContractNumber)
           .NotEmpty().WithMessage("Contract number is required.")
           .Length(5).WithMessage("Contract number must have exactly 5 characters.");

        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .LessThanOrEqualTo(c => c.EndDate).WithMessage("Start date must be less than or equal to end date.");

        RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(c => c.StartDate).WithMessage("End date must be greater than or equal to start date.");

        RuleFor(c => c.Status)
            .IsInEnum().WithMessage("Invalid contract status.");
    }
}

