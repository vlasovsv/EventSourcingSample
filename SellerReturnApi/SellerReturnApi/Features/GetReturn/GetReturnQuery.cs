using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.GetReturn
{
    public class GetReturnQuery : IRequest<SellerReturn>
    {
        public long Id { get; set; }
    }
}