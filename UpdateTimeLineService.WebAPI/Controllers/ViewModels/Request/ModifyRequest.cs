namespace UpdateTimeLineService.WebAPI.Controllers.ViewModels.Request
{
    public record ModifyRequest(Guid Id ,string? Description,DateTime? DateTime);
}
