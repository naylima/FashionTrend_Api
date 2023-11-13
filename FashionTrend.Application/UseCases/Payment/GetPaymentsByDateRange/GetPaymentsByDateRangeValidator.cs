using System;
using FluentValidation;

public class GetPaymentsByDateRangeValidator : AbstractValidator<GetPaymentsByDateRangeRequest>
{
	public GetPaymentsByDateRangeValidator() 
	{
        RuleFor(request => request.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .LessThan(request => request.EndDate).WithMessage("Start date must be before end date.");

        RuleFor(request => request.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(request => request.StartDate).WithMessage("End date must be after start date.");
    }
}

