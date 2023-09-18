using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Fergun.Interactive;
using LeagueStatusBot.Helpers;

namespace LeagueStatusBot.Services;


public static class ButtonFactory
{
    public static ButtonOption<string>[] CreateButtonOptions()
    {
        return new ButtonOption<string>[]
        {
                new("Attack", ButtonStyle.Primary, false),
                new("Defend", ButtonStyle.Secondary, false),
                new("Ability", ButtonStyle.Success, false)
        };
    }

    public static ButtonOption<string>[] CreateDisplayOnlyButtonOptions()
    {
        return new ButtonOption<string>[]
        {
                new("Attack", ButtonStyle.Primary, true),
                new("Defend", ButtonStyle.Secondary, true),
                new("Ability", ButtonStyle.Success, true)
        };
    }

    public static ButtonOption<string>[] CreateAcceptDenyButtonOptions()
    {
        return new ButtonOption<string>[]
            {
                new("Accept", ButtonStyle.Primary, false),
                new("Deny", ButtonStyle.Secondary, false)
            };
    }

    public static ButtonSelection<string> CreateButtonSelection(ButtonOption<string>[] options, PageBuilder pageBuilder, SocketUser currentUser)
    {
        return new ButtonSelectionBuilder<string>()
            .WithOptions(options)
            .WithStringConverter(x => x.Option)
            .WithActionOnCancellation(ActionOnStop.DisableInput)
            .WithSelectionPage(pageBuilder)
            .AddUser(currentUser)
            .Build();
    }

    public static ButtonSelection<string> CreateChallengeButtons(ButtonOption<string>[] acceptDeny, PageBuilder pageBuilder, PageBuilder cancelBuilder, SocketUser user)
    {
        return new ButtonSelectionBuilder<string>()
            .WithOptions(acceptDeny)
            .WithStringConverter(x => x.Option)
            .WithSelectionPage(pageBuilder)
            .WithActionOnCancellation(ActionOnStop.ModifyMessage | ActionOnStop.DeleteInput)
            .WithCanceledPage(cancelBuilder)
            .AddUser(user)
            .Build();
    }

}