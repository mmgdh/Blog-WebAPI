using DomainCommon;

namespace TimeLineService.Domain.Entities
{
    public class TimeLine: AggregateRootEntity
    {
        public string Description { get; set; } = "";

        public DateTime Time { get; set; }

        public EnumTimeLine LineType { get; set; }

        public static TimeLine Create(EnumTimeLine lineType, string updateDescription,DateTime? dateTime=null)
        {
            TimeLine TimeLine = new TimeLine();
            TimeLine.Description = updateDescription;
            TimeLine.Time = dateTime ?? DateTime.Now;
            TimeLine.LineType = lineType;
            return TimeLine;
        }
    }
}
