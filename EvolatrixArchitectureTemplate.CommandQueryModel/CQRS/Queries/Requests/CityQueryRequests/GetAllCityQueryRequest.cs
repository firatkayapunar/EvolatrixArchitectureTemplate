using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Responses.CityQueryResponses;
using MediatR;

namespace EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Requests.CityQueryRequests
{
    public class GetAllCityQueryRequest : IRequest<List<GetAllCityQueryResponse>>
    { }
}
