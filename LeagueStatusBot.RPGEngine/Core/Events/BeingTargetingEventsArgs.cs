﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueStatusBot.RPGEngine.Core.Events
{
    public class BeingTargetingEventsArgs : EventArgs
    {
        public ulong CharacterId { get; set; }
        public string DeathSentence { get; set; }

        public BeingTargetingEventsArgs(ulong CharacterId, string DeathSentence)
        {
            this.CharacterId = CharacterId;
            this.DeathSentence = DeathSentence;
        }
    }
}
