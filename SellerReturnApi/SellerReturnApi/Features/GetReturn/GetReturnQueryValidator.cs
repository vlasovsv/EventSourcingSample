using FluentValidation;

namespace SellerReturnApi.Features.GetReturn
{
    public class GetReturnQueryValidator : AbstractValidator<GetReturnQuery>
    {
        public GetReturnQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}