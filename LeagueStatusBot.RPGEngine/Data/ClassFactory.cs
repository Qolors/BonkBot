﻿using LeagueStatusBot.RPGEngine.Core.Engine;
using LeagueStatusBot.RPGEngine.Data.Classes.Adventurer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueStatusBot.RPGEngine.Data
{
    public static class ClassFactory
    {
        //TODO --> IMPLEMENT CLASS TEMPLATE JSON FILE
        public static Being CreateAdventurer()
        {
            return new Adventurer()
            { 
                BaseStats = new Stats() { Strength = 12, Endurance = 12 },
                ClassName = "Adventurer",
            };
        }
    }
}