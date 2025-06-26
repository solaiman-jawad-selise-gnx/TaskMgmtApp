using FluentValidation;

namespace Application.Features.TaskMgmt.Commands.Validators;

public class UpdateTaskValidator: AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.taskId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be a positive integer.");
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(16, 512).WithMessage("Description must be between 16 and 512 characters long.");
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(2, 64).WithMessage("Title must be between 2 and 64 characters long.");
        RuleFor(command => command.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
        RuleFor(command => command.AssignedUserId)
            .NotEmpty().WithMessage("Assigned user ID is required.")
            .GreaterThan(0).WithMessage("Assigned user ID must be a positive integer.");
        RuleFor(command => command.CreatedByUserId)
            .NotEmpty().WithMessage("Created by user ID is required.")
            .GreaterThan(0).WithMessage("Created by user ID must be a positive integer.");
        RuleFor(command => command.TeamId)
            .NotEmpty().WithMessage("Team ID is required.")
            .GreaterThan(0).WithMessage("Team ID must be a positive integer.");
    }
    
}