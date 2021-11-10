using FluentValidation;

namespace SellerReturnApi.Features.ReturnToSeller
{
    public class ReturnToSellerCommandValidator : AbstractValidator<ReturnToSellerCommand>
    {
        public ReturnToSellerCommandValidator()
        {
            RuleFor(x => x.ArticleId)
                .GreaterThan(0);

            RuleFor(x => x.Moment)
                .NotEmpty();
        }
    }
}