using Common.Models;
using MediatR;

namespace Common.Query
{
    public class GetLookupQuery : IRequest<LookupViewModel>
    {
    }
}