using FluentValidation;

namespace Application.Features.UserMgmt.Queries.Validators;

public class GetUserByIdValidator: AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");
    }
}