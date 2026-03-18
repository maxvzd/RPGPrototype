using System;
using System.Collections.Generic;

namespace NPC
{
    public static class Entities
    {
        private static PlayerEntity _player;
        private static readonly Dictionary<Guid, Entity> _entities =  new();
        private static readonly Dictionary<Guid, NpcEntity> _npcs =  new();
        public static IReadOnlyDictionary<Guid, Entity> List => _entities;
        public static IReadOnlyDictionary<Guid, NpcEntity> Npcs => _npcs;
        public static PlayerEntity Player => _player;
        
        
        public static void Register(NpcEntity npc)
        {
            _entities.Add(npc.Id, npc);
            _npcs.Add(npc.Id, npc);
        }

        public static void Register(PlayerEntity player)
        {
            if (_player is not null) throw new Exception("Player already registered");
            _player = player;
            _entities.Add(player.Id, player);
        }
    }
}