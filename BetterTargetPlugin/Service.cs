using Dalamud.Game;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin.Services;

namespace BetterTargetPlugin
{
    internal class Service
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [PluginService] public static IObjectTable Objects { get; private set; }
        [PluginService] public static ITargetManager Targets { get; private set; }
        [PluginService] public static IClientState ClientState { get; private set; }
        [PluginService] public static SigScanner SigScanner { get; private set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
