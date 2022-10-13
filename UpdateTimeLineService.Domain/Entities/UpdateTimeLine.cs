using DomainCommon;

namespace UpdateTimeLineService.Domain.Entities
{
    public class UpdateTimeLine: AggregateRootEntity
    {
        public string UpdateDescription { get; set; } = "";

        public DateTime UpdateDate { get; set; }

        public static UpdateTimeLine Create(string updateDescription,DateTime? dateTime=null)
        {
            UpdateTimeLine updateTimeLine = new UpdateTimeLine();
            updateTimeLine.UpdateDescription = updateDescription;
            updateTimeLine.UpdateDate = (dateTime ?? DateTime.Now).Date;
            return updateTimeLine;
        }
    }
}
