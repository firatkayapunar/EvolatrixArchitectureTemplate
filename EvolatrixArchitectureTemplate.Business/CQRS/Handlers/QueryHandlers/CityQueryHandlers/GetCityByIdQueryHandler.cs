using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Requests.CityQueryRequests;
using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Responses.CityQueryResponses;
using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.PersistenceContract;
using MediatR;

namespace EvolatrixArchitectureTemplate.Business.CQRS.Handlers.QueryHandlers.CityQueryHandlers
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQueryRequest, GetCityByIdQueryResponse>
    {
        private readonly IRepository<City> _repository;
        public GetCityByIdQueryHandler(IRepository<City> repository)
        {
            _repository = repository;
        }
        public async Task<GetCityByIdQueryResponse> Handle(GetCityByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);

            return new GetCityByIdQueryResponse
            {
                Name = value.Name
            };
        }
    }
}
