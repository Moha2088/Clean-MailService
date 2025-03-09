using FluentValidation;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Validation
{
    public class CreateUserValidator : AbstractValidator<UserCreateDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .Must(x => x.Contains('@'));

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .Must(HaveAtLeastOneInteger)
                .WithMessage("Password should be atleast 8 characters and contain a number");
        }

        private bool HaveAtLeastOneInteger(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
    
    
    public class CreateUserPipelineBehaviour : IPipelineBehavior<UserCreateDto, UserGetResponseDto>
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