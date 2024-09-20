using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Requests.CityQueryRequests;
using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Responses.CityQueryResponses;
using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.PersistenceContract;
using MediatR;

namespace EvolatrixArchitectureTemplate.Business.CQRS.Handlers.QueryHandlers.CityQueryHandlers
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCityQueryRequest, List<GetAllCityQueryResponse>>
    {
        private readonly IRepository<City> _repository;
        public GetAllCitiesQueryHandler(IRepository<City> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllCityQueryResponse>> Handle(GetAllCityQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAllCityQueryResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
