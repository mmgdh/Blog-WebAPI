using TimeLineService.Domain.Entities;

namespace TimeLineService.WebAPI.Controllers.ViewModels.Request
{
    public record ModifyRequest(Guid Id ,string Description,EnumTimeLine lineType);
}
