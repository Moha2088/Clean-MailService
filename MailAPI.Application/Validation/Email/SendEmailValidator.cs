using FluentValidation;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MediatR;

namespace MailAPI.Application.Validation.Email
{
    public sealed class SendEmailValidator : AbstractValidator<EmailCreateDto>
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

    public sealed class SendMailPipelineBehaviour : IPipelineBehavior<EmailCreateDto, EmailGetResponseDto>
    {
        public async Task<EmailGetResponseDto> Handle(EmailCreateDto request, RequestHandlerDelegate<EmailGetResponseDto> next, CancellationToken cancellationToken)
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
