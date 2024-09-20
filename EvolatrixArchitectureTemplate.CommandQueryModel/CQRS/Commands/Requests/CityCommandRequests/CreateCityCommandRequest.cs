using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using MediatR;

namespace EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests
{
    public class CreateCityCommandRequest : IRequest<CreateCityCommandResponse>
    {
        public string Name { get; set; }
    }
}
