using TimeLineService.Domain.Entities;

namespace TimeLineService.WebAPI.Controllers.ViewModels.Response
{
    public record TimeLineResponse(DateTime UpdateTiem,string Description,EnumTimeLine lineType)
    {
        public static List<TimeLineResponse> CreateList(List<TimeLine> TimeLines)
        {
            return TimeLines.Select(x =>
             {
                 return new TimeLineResponse(x.Time, x.Description,x.LineType);
             }).ToList();
        }
    }
}
