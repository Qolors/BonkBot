﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using LeagueStatusBot.RPGEngine.Data.Contexts;
using LeagueStatusBot.RPGEngine.Data.Repository;
using LeagueStatusBot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueStatusBot;

public class Startup
{
    private DiscordSocketClient _client;
        
    public async Task Initialize()
    {
        await using var services = ConfigureServices();

        _client = services.GetRequiredService<DiscordSocketClient>();
        _client.Ready += OnReady;
        _client.Log += LogAsync;

        services.GetRequiredService<CommandService>().Log += LogAsync;

        await services.GetRequiredService<InteractionHandlerService>().InitializeAsync();
        await services.GetRequiredService<CommandHandlingService>().InitializeAsync();
        await services.GetRequiredService<NasaSchedulerService>().InitializeAsync();
        await services.GetRequiredService<GameControllerService>().InitializeAsync();
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
        await _client.StartAsync();

        await Task.Delay(Timeout.Infinite);
    }
        
    private Task OnReady()
    {

        Console.WriteLine($"Connected to these servers as '{_client.CurrentUser.Username}': ");
        foreach (var guild in _client.Guilds) 
            Console.WriteLine($"- {guild.Name}");

        _client.SetGameAsync(Environment.GetEnvironmentVariable("DISCORD_BOT_ACTIVITY") ?? "I'm alive!", 
            type: ActivityType.CustomStatus);
        Console.WriteLine($"Activity set to '{_client.Activity.Name}'");

        return Task.CompletedTask;
    }

    private static Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());

        return Task.CompletedTask;
    }

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                MessageCacheSize = 50,
                LogLevel = LogSeverity.Info,
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = Discord.Commands.RunMode.Async,
                CaseSensitiveCommands = false,
            }))
            .AddDbContext<GameDbContext>(options =>
            {
                options.UseSqlite("Data Source=/app/game.db");
            })
            .AddSingleton<CommandHandlingService>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandlerService>()
            .AddSingleton<NasaSchedulerService>()
            .AddSingleton<GameControllerService>()
            .AddSingleton<PlayerRepository>()
            .AddSingleton<ItemRepository>()
            .BuildServiceProvider();
    }
}