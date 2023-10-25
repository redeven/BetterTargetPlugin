using System.Linq;
using System.Numerics;
using Dalamud.Game.Command;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.IoC;
using Dalamud.Plugin;
using GameObjectStruct = FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject;
using Dalamud.Plugin.Services;

namespace BetterTargetPlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "BetterTarget";
        private const string CommandName = "/btarget";
        private DalamudPluginInterface PluginInterface { get; init; }
        private ICommandManager CommandManager { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] ICommandManager commandManager
        ) {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;
            pluginInterface.Create<Service>();
            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Allows targetting closest entity that matches any term of a space-delimited list"
            });
        }

        public void Dispose()
        {
            CommandManager.RemoveHandler(CommandName);
        }

        private unsafe void OnCommand(string command, string args)
        {
            GameObject? closestMatch = null;
            var closestDistance = float.MaxValue;
            var player = Service.ClientState.LocalPlayer;
            var searchTerms = args.Split(" ");
            foreach (var actor in Service.Objects)
            {
                if (actor == null) continue;
                if (player == null) continue;
                var valueFound = searchTerms.Any(searchName => actor.Name.TextValue.ToLowerInvariant().Contains(searchName.ToLowerInvariant()));
                if (valueFound && ((GameObjectStruct*)actor.Address)->GetIsTargetable())
                {
                    var distance = Vector3.Distance(player.Position, actor.Position);
                    if (closestMatch == null)
                    {
                        closestMatch = actor;
                        closestDistance = distance;
                        continue;
                    }
                    if (closestDistance > distance)
                    {
                        closestMatch = actor;
                        closestDistance = distance;
                    }
                }
            }

            if (closestMatch != null)
            {
                Service.Targets.Target = closestMatch;
            }
        }
    }
}
