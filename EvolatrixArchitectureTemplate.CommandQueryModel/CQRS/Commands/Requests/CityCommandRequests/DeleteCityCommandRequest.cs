using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using MediatR;

namespace EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests
{
    public class DeleteCityCommandRequest : IRequest<DeleteCityCommandResponse>
    {
        public int Id { get; set; }
    }
}
