using FluentValidation;

namespace SellerReturnApi.Features.MoveReturn
{
    public class MoveReturnCommandValidator : AbstractValidator<MoveReturnCommand>
    {
        public MoveReturnCommandValidator()
        {
            RuleFor(x => x.ArticleId)
                .GreaterThan(0);

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0);
            
            RuleFor(x => x.CellId)
                .GreaterThan(0);
            
            RuleFor(x => x.Moment)
                .NotEmpty();
        }
    }
}