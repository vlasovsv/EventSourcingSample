using FluentValidation;

namespace SellerReturnApi.Features.CreateReturn
{
    public class CreateReturnCommandValidator : AbstractValidator<CreateReturnCommand>
    {
        public CreateReturnCommandValidator()
        {
            RuleFor(x => x.ArticleId)
                .GreaterThan(0);

            RuleFor(x => x.ArticleName)
                .NotEmpty();
            
            RuleFor(x => x.WarehouseId)
                .GreaterThan(0);
            
            RuleFor(x => x.DropOffPointId)
                .GreaterThan(0);
        }
    }
}