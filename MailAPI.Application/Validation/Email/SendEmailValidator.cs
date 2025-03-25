using FluentValidation;
using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MediatR;

namespace MailAPI.Application.Validation.Email
{
    public sealed class SendEmailValidator : AbstractValidator<EmailCreateCommand>
    {
        public SendEmailValidator()
        {
            RuleFor(x => x.To)
                .NotEmpty()
                .Must(x => x.Contains('@'))
                .WithMessage("Please enter a valid email!");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Please enter a subject!");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage("Body can't be empty!");
        }
    }
}
