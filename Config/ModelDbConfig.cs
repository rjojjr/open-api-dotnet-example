using System;
namespace open_ai_example.Config
{
	public class ModelDbConfig
	{
		public ModelDbConfig()
		{
		}

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Host { get; set; } = null!;

        public int Port { get; set; } = 27017;
    }
}

