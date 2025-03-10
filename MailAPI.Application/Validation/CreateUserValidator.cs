using FluentValidation;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Validation
{
    public sealed class CreateUserValidator : AbstractValidator<UserCreateDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .Must(x => x.Contains('@'));

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .Must(HaveAtLeastOneInteger)
                .Must(HaveAtleastOneUpperAndOneLower)
                .Must(HaveAtleastOneSpecialChar)
                .WithMessage("Password should be at least 8 characters and contain a number and a special character");
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
        
    
    public sealed class CreateUserPipelineBehaviour : IPipelineBehavior<UserCreateDto, UserGetResponseDto>
    {
        public async Task<UserGetResponseDto> Handle(UserCreateDto dto, RequestHandlerDelegate<UserGetResponseDto> next, CancellationToken cancellationToken)
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