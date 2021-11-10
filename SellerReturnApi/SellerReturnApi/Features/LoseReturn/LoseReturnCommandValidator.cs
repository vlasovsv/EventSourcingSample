using FluentValidation;

namespace SellerReturnApi.Features.LoseReturn
{
    public class LoseReturnCommandValidator : AbstractValidator<LoseReturnCommand>
    {
        public LoseReturnCommandValidator()
        {
            RuleFor(x => x.ArticleId)
                .GreaterThan(0);

            RuleFor(x => x.Moment)
                .NotEmpty();
        }
    }
}