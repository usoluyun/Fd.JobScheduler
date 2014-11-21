using HubRoute.Domain;
using Quartz;

namespace HubRoute
{
    public class TriggerTypeExtractor
    {
        public CronTriggerType GetFor(ITrigger trigger)
        {
            //var simpleTrigger = trigger as ISimpleTrigger;
            //if (simpleTrigger != null)
            //{
            //     return new SimpleTriggerType(
            //         simpleTrigger.RepeatCount, 
            //         (long) simpleTrigger.RepeatInterval.TotalMilliseconds,
            //         simpleTrigger.TimesTriggered);
            //}

            var cronTrigger = trigger as ICronTrigger;
            if (cronTrigger != null)
            {
                return new CronTriggerType(cronTrigger.CronExpressionString);
            }

            return new CronTriggerType("unknown");
        }
    }
}