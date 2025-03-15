using FluentValidation;
using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MediatR;

namespace MailAPI.Application.Validation.User
{
    public sealed class CreateUserValidator : AbstractValidator<UserCreateCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .Must(x => x.Contains('@'))
                .WithMessage("Please enter a valid email!");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .Must(HaveAtLeastOneInteger)
                .Must(HaveAtleastOneUpperAndOneLower)
                .Must(HaveAtleastOneSpecialChar)
                .WithMessage("Password should be at least 8 characters and contain a number and a special character!");
        }   

        private bool HaveAtLeastOneInteger(string password)
        {
            return password.Any(char.IsDigit);
        }

        private bool HaveAtleastOneUpperAndOneLower(string password)
        {
            return password.Any(ch => char.IsUpper(ch)) && password.Any(ch => char.IsLower(ch));
        }

        private bool HaveAtleastOneSpecialChar (string password)
        {
            char[] specialChars = { '[','@','_', '-', '!', '#', '$', '%', '^', 
                '&', '*', '(', ')', '<', '>', '?', '}', '{', '~', ':', ']'};

            return password.Any(ch => specialChars.Any(spCh => ch.Equals(spCh)));
        }
    }
        
    
    public sealed class CreateUserPipelineBehaviour : IPipelineBehavior<UserCreateCommand, UserGetResponseDto>
    {
        public async Task<UserGetResponseDto> Handle(UserCreateCommand dto, RequestHandlerDelegate<UserGetResponseDto> next, CancellationToken cancellationToken)
        {
            var createUserValidator = new CreateUserValidator();
            var validationResult = await createUserValidator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var response = await next();
            return response;
        }
    }
}