using FluentValidation;
using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
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

    public sealed class SendMailPipelineBehaviour : IPipelineBehavior<EmailCreateCommand, EmailGetResponseDto>
    {
        public async Task<EmailGetResponseDto> Handle(EmailCreateCommand request, RequestHandlerDelegate<EmailGetResponseDto> next, CancellationToken cancellationToken)
        {
            var sendMailValidator = new SendEmailValidator();
            var validationResult = await sendMailValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var response = await next();
            return response;
        }
    }
}
