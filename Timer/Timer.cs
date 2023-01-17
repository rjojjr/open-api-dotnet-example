using System;
namespace open_ai_example.ai.Timer
{
	public class Timer
	{
		private long start = 0;

		public void StartTimer()
		{
			start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

		public long GetTimeElasped()
		{
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - start;
        }

		public bool IsStarted()
		{
			return start != 0;
		}

        public Timer()
		{
		}

		public static Timer TimerFactory(bool start)
		{
			var timer = new Timer();
			if (start)
			{
				timer.StartTimer();
			}

			return timer;
		}
	}
}

