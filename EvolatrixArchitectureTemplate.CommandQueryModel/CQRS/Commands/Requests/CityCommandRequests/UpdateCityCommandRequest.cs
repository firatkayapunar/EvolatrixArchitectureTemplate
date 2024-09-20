using EvolatrixArchitectureTemplate.CommandQueryModels.CQRS.Commands.Responses.CityCommandResponses;
using MediatR;

namespace EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests
{
    public class UpdateCityCommandRequest : IRequest<UpdateCityCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
