using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests;
using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using EvolatrixArchitectureTemplate.Domain.Entities;
using EvolatrixArchitectureTemplate.PersistenceContract;
using MediatR;

namespace EvolatrixArchitectureTemplate.Business.CQRS.Handlers.CommandHandlers.CityCommandHandlers
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommandRequest, UpdateCityCommandResponse>
    {
        private readonly IRepository<City> _repository;
        public UpdateCityCommandHandler(IRepository<City> repository)
        {
            _repository = repository;
        }
        public async Task<UpdateCityCommandResponse> Handle(UpdateCityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _repository.GetByIdAsync(request.Id);

                if (value != null)
                {
                    value.Id = request.Id;
                    value.Name = request.Name;

                    await _repository.UpdateAsync(value);

                    return new UpdateCityCommandResponse
                    {
                        Message = "Update process was successful.\r\n",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new UpdateCityCommandResponse
                    {
                        Message = "İlgili Kayıt Bulunamadı.",
                        IsSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateCityCommandResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
