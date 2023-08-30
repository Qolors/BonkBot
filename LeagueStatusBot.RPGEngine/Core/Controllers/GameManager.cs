﻿using LeagueStatusBot.Common.Models;
using LeagueStatusBot.RPGEngine.Core.Engine;
using System.Linq;
using LeagueStatusBot.RPGEngine.Core.Events;
using LeagueStatusBot.RPGEngine.Factories;
using LeagueStatusBot.RPGEngine.Factories.Monsters.Abilities;
using LeagueStatusBot.RPGEngine.Factories.Monsters;

namespace LeagueStatusBot.RPGEngine.Core.Controllers
{
    public class GameManager
    {
        public Encounter? CurrentEncounter { get; set; }
        public List<string> EventHistory { get; set; } = new List<string>(); // This can be more detailed with a custom class
        public bool IsGameStarted() => CurrentEncounter == null ? false : true;

        public event EventHandler GameStarted;
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public event EventHandler<string> GameEvent;
        public event EventHandler<CharacterDeathEventArgs> GameDeath;
        public event EventHandler<Being> TurnStarted;
        public event EventHandler<TurnActionsEventArgs> TurnEnded;
        public event EventHandler RoundEnded;
        public event EventHandler RoundStarted;

        public async Task StartGameAsync(List<Being> beings)
        {
            Party party = new Party();

            foreach(var being in beings)
            {
                being.IsHuman = true;
                being.InitializeAbilities();
                party.AddPartyMember(being);
            }

            CurrentEncounter = new Encounter
            {
                PlayerParty = party,
                EncounterParty = GenerateMonsters(party.Members.Count, party.AverageGearScore())
            };

            GameStarted?.Invoke(this, EventArgs.Empty);

            await Task.Delay(5000);

            await SpawnEncounterAsync();
        }

        public static Being AssignRandomClass(string className)
        {
            return className switch
            {
                "Adventurer" => ClassFactory.CreateAdventurer(),
                "Apprentice" => ClassFactory.CreateApprentice(),
                "Vagabond" => ClassFactory.CreateVagabond(),
                null => ClassFactory.CreateAdventurer(),
                _ => ClassFactory.CreateAdventurer(),
            };
        }

        private async Task SpawnEncounterAsync()
        {

            CurrentEncounter.EncounterEnded += OnEncounterEnded;

            CurrentEncounter.TurnStarted += OnTurnStarted;
            CurrentEncounter.TurnEnded += OnTurnEnded;

            CurrentEncounter.PartyDeath += OnPartyMemberDeath;
            CurrentEncounter.PartyAction += OnPartyAction;

            CurrentEncounter.RoundEnded += OnRoundEnded;
            CurrentEncounter.RoundStarted += OnRoundStarted;

            CurrentEncounter.PartyMemberEffect += OnPartyEffect;
            CurrentEncounter.PartyMemberEffectRemoval += OnPartyEffectRemoval;

            Console.WriteLine("Spawned");

            await CurrentEncounter.StartEncounterAsync();
        }

        private Party GenerateMonsters(int playerCount, int gearScore)
        {
            var party = new Party();

            for (int i = 0; i < playerCount; i++)
            {
                party.AddPartyMember(MonsterLoader.GetRandomEnemy(gearScore));
            }

            return party;
        }

        private void OnEncounterEnded(object sender, GameEndedEventArgs e)
        {
            GameEnded?.Invoke(sender, e);

            CurrentEncounter.EncounterEnded -= OnEncounterEnded;
            CurrentEncounter.TurnStarted -= OnTurnStarted;
            CurrentEncounter.TurnEnded -= OnTurnEnded;
            CurrentEncounter.PartyDeath -= OnPartyMemberDeath;
            CurrentEncounter.PartyAction -= OnPartyAction;
            CurrentEncounter.RoundEnded -= OnRoundEnded;
            CurrentEncounter.RoundStarted -= OnRoundStarted;
            CurrentEncounter.PartyMemberEffect -= OnPartyEffect;
            CurrentEncounter.PartyMemberEffectRemoval -= OnPartyEffectRemoval;

            EventHistory.Clear();

            CurrentEncounter = null;
        }

        private void OnTurnStarted(object sender, Being e)
        {
            TurnStarted?.Invoke(sender, e);
        }

        private void OnTurnEnded(object sender, EventArgs e)
        {
            TurnEnded?.Invoke(sender, new TurnActionsEventArgs(EventHistory, CurrentEncounter.CurrentTurn, CurrentEncounter.CurrentTurn.LastActionPerformed));
            EventHistory.Clear();
        }

        private void OnPartyMemberDeath(object sender, CharacterDeathEventArgs e)
        {
            if (EventHistory.Count >= 14)
            {
                EventHistory.RemoveAt(0);
            }
            EventHistory.Add(e.DeathSentence);
            GameDeath?.Invoke(sender, e);
        }

        private void OnPartyAction(object sender, string e)
        {
            if (EventHistory.Count >= 14)
            {
                EventHistory.RemoveAt(0);
            }
            EventHistory.Add(e);
            GameEvent?.Invoke(sender, e);
        }

        private void OnPartyEffect(object sender, string e)
        {
            EventHistory.Add(e);
            GameEvent?.Invoke(sender, e);
        }

        private void OnPartyEffectRemoval(object sender, string e)
        {
            EventHistory.Add(e);
            GameEvent?.Invoke(sender, e);
        }

        private void OnRoundEnded(object sender, EventArgs e)
        {
            RoundEnded?.Invoke(sender, e);
        }

        private void OnRoundStarted(object sender, EventArgs e)
        {
            RoundStarted?.Invoke(sender, e);
        }

        public Being? CheckIfActivePlayer(ulong id)
        {
            if (CurrentEncounter?.CurrentTurn.DiscordId != id) return null;

            return CurrentEncounter?.CurrentTurn;
        }

        public List<string> GetEnemyPartyNames()
        {
            var enemies = new List<string>();
            foreach (var enemy in CurrentEncounter.EncounterParty.Members)
            {
                string selector = $"{enemy.Name} - ({enemy.HitPoints}/{enemy.MaxHitPoints}) HitPoints";
                enemies.Add(selector);
            }

            return enemies;
        }

        public List<string> GetPlayerPartyNames()
        {
            var allies = new List<string>();
            foreach (var ally in CurrentEncounter.PlayerParty.Members)
            {
                string selector = $"{ally.Name} - ({ally.HitPoints}/{ally.MaxHitPoints}) HitPoints";
                allies.Add(selector);
            }

            return allies;
        }

        public void SetPlayerTarget(Being player, string name)
        {
            Being? enemy = CurrentEncounter?.EncounterParty?.Members
                .FirstOrDefault(e => e.Name == name);

            if (enemy == null)
            {
                Being? ally = CurrentEncounter?.PlayerParty?.Members
                    .FirstOrDefault(a => a.Name == name);

                player?.SetTarget(ally);
            }
            else
            {
                player?.SetTarget(enemy);
            }

            
        }

    }
}
