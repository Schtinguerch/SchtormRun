using System;
using System.Diagnostics;
using DiscordRPC;
using DiscordRPC.Logging;
using SchtormRun.Properties;

namespace SchtormRun
{
    public static class DiscordRPC
    {
        private static DiscordRpcClient _client;
        private static bool _enabled;

        public static bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                    Initialize();
                else
                    Shutdown();

                _enabled = value;
            }
        }

        public static void Initialize()
        {
            _client = new DiscordRpcClient(Settings.Default.DiscordAppId);
            _client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            _client.OnReady += (s, e) => Debug.WriteLine($"Received ready from user: {e.User.Username}");
            _client.OnPresenceUpdate += (s, e) => Debug.WriteLine($"Received update: {e.Presence}");

            _client.Initialize();
        }

        public static void Shutdown()
        {
            _client.Deinitialize();
            _client.Dispose();

            _client = null;
        }

        public static void Update(string mainActivity, 
            string additionalInfo = "", 
            string imageKey = "", 
            Button[] buttons = null) =>
            
            _client?.SetPresence(new RichPresence()
            {
                Details = mainActivity,
                State = additionalInfo,

                Buttons = buttons,
            });
    }
}
