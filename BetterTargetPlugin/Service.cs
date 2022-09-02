using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;

namespace BetterTargetPlugin
{
    internal class Service
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [PluginService] public static ObjectTable Objects { get; private set; }
        [PluginService] public static TargetManager Targets { get; private set; }
        [PluginService] public static ClientState ClientState { get; private set; }
        [PluginService] public static SigScanner SigScanner { get; private set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
