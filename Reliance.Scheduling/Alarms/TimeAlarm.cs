using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
    internal class TimeAlarm
    {
        public delegate RelianceMetadata? TimeAlarmHandler(RelianceMetadata? arg = null);
        public event TimeAlarmHandler? Notifier;

        private TimeAlarm _instance = new TimeAlarm();
        private readonly int _secondInterval = 60;

        public TimeAlarm Instance { get { return _instance ?? new TimeAlarm(); } }

        private TimeAlarm()
        {
            Thread timeAlarmThread = new Thread(Waiting);
        }

        private void Waiting()
        {
            for (; ; )
            {
                Thread.Sleep(_secondInterval);
                Notifier?.Invoke();
                RelianceMetadata execution = new RelianceMetadata(this, [$"Интервал: {_secondInterval} сек."], RelianceMetadataStatus.Information);
            }
        }
    }
}
