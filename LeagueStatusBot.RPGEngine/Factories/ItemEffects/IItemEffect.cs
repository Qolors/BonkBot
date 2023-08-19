﻿using LeagueStatusBot.RPGEngine.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueStatusBot.RPGEngine.Factories.ItemEffects
{
    public interface IItemEffect
    {
        public int EffectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public void Execute(Being being);
    }
}