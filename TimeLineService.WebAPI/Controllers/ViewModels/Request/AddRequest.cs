using FluentValidation;
using TimeLineService.Domain.Entities;

namespace TimeLineService.WebAPI.Controllers.ViewModels.Request
{
    public record AddRequest(string Description,DateTime DateTime,EnumTimeLine lineType);

    public class AddRequestValidator : AbstractValidator<AddRequest>
    {
        public AddRequestValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            //异步报错了，后续再解决
            RuleFor(x => x.lineType).Must((lineType) => Enum.IsDefined(typeof(EnumTimeLine),lineType))
                .WithMessage(c => $"所选时间线类型不在范围，请重新选择");
        }
    }
}
