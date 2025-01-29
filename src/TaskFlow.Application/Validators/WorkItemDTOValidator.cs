using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validators
{
    public class WorkItemDTOValidator : AbstractValidator<WorkItemDTO>
    {
        public WorkItemDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("A data de vencimento deve ser futura.");
        }
    }
}
