using Frostspark.API.Utilities;
using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands.Implementations
{
    [CommandName("god", "godmode")]
    [CommandDescription("Enables or disables godmode on a player")]
    [CommandPermission("essentials.commands.god")]
    public class GodCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void ToggleSelf()
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            var new_state = ply.Godmode = !ply.Godmode;

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var palegreen = EssentialsPlugin.Server.Colors.SuccessPale;
            var palered = EssentialsPlugin.Server.Colors.ErrorPale;

            Sender.SendFormattedMessage($"Godmode is now {(new_state ? (FormattableString)$"{palegreen}enabled{infocol}" : (FormattableString)$"{palered}disabled{infocol}")}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"{(new_state ? "Enabled" : "Disabled")} godmode.");
        }

        [CommandCallback]
        public void ToggleOther(Player ply)
        {
            var new_state = ply.Godmode = !ply.Godmode;

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var palegreen = EssentialsPlugin.Server.Colors.SuccessPale;
            var palered = EssentialsPlugin.Server.Colors.ErrorPale;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            ply.SendFormattedMessage($"{emphasis}{Sender.LogName}{infocol} {(new_state ? (FormattableString)$"{palegreen}enabled{infocol}" : (FormattableString)$"{palered}disabled{infocol}")} godmode on you.", infocol);
            ply.SendFormattedMessage($"You {(new_state ? (FormattableString)$"{palegreen}enabled{infocol}" : (FormattableString)$"{palered}disabled{infocol}")} godmode for {emphasis}{ply.Name}{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"{(new_state ? "Enabled" : "Disabled")} godmode for {ply.Name}");

        }
    }
}
