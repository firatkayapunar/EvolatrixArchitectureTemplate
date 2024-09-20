using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Responses.CityQueryResponses;
using MediatR;

namespace EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Requests.CityQueryRequests
{
    public class GetCityByIdQueryRequest : IRequest<GetCityByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
