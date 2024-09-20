using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests;
using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.PersistenceContract;
using MediatR;

namespace EvolatrixArchitectureTemplate.Business.CQRS.Handlers.CommandHandlers.CityCommandHandlers
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommandRequest, CreateCityCommandResponse>
    {
        private readonly IRepository<City> _repository;
        public CreateCityCommandHandler(IRepository<City> repository)
        {
            _repository = repository;
        }
        public async Task<CreateCityCommandResponse> Handle(CreateCityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.CreateAsync(new City
                {
                    Name = request.Name,
                });

                return new CreateCityCommandResponse
                {
                    Message = "Registration process was successful.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new CreateCityCommandResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
