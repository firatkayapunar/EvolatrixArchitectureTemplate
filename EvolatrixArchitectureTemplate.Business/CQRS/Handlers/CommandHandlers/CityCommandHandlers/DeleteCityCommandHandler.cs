using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests;
using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.PersistenceContract;
using MediatR;

namespace EvolatrixArchitectureTemplate.Business.CQRS.Handlers.CommandHandlers.CityCommandHandlers
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommandRequest, DeleteCityCommandResponse>
    {
        private readonly IRepository<City> _repository;
        public DeleteCityCommandHandler(IRepository<City> repository)
        {
            _repository = repository;
        }
        public async Task<DeleteCityCommandResponse> Handle(DeleteCityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _repository.GetByIdAsync(request.Id);

                await _repository.DeleteAsync(value);

                return new DeleteCityCommandResponse
                {
                    Message = "Deletion process was successful.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new DeleteCityCommandResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
