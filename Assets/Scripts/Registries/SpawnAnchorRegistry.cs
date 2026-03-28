using System.Collections.Generic;
using DataPersistence.Spawning;

namespace Registries
{
    public static class SpawnAnchorRegistry
    {
        private static readonly Dictionary<string, SpawnAnchor> _spawnAnchors = new();
        public static IReadOnlyDictionary<string, SpawnAnchor> SpawnAnchors => _spawnAnchors;
    
        public static bool Register(SpawnAnchor spawnAnchor)
        {
            return _spawnAnchors.TryAdd(spawnAnchor.Key, spawnAnchor);
        }
    }
}
