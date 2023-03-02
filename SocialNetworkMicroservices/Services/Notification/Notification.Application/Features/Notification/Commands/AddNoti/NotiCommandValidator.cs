using FluentValidation;


namespace Notification.Application.Features.Notification.Commands.AddNoti
{
    public class NotiCommandValidator : AbstractValidator<AddNotiCommand>
    {
        public NotiCommandValidator()
        {
            RuleFor(p => p.NoiDung)
                .NotEmpty().WithMessage("{NoiDung} is required.");

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{UserName} is required.");

            RuleFor(p => p.UsernameComment)
                .NotEmpty().WithMessage("{UsernameComment} is required.");

            RuleFor(p => p.PostId)
                .NotEmpty().WithMessage("{PostId} is required.");
        }
    }
}
