using System;
using System.Collections.Generic;

namespace NPC
{
    public static class EntitiesRegistry
    {
        private static readonly Dictionary<Guid, Entity> Entities =  new();
        private static readonly Dictionary<Guid, NpcEntity> Npcs =  new();
        public static IReadOnlyDictionary<Guid, Entity> Dictionary => Entities;
        public static IReadOnlyDictionary<Guid, NpcEntity> NpcDictionary => Npcs;
        public static PlayerEntity Player { get; private set; }


        public static void Register(NpcEntity npc)
        {
            Entities.Add(npc.Id, npc);
            Npcs.Add(npc.Id, npc);
        }

        public static void Register(PlayerEntity player)
        {
            if (Player is not null) throw new Exception("Player already registered");
            Player = player;
            Entities.Add(player.Id, player);
        }
    }
}