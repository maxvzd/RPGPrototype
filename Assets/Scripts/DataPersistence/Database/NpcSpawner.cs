using DataPersistence.Spawning;
using NPC;
using Registries;
using UnityEngine;

namespace DataPersistence.Database
{
    public class NpcSpawner : MonoBehaviour
    {
        private DatabaseRepository _database;

        private void Start()
        {
            _database = new DatabaseRepository();
            var npcs = _database.GetNpcs();
            var prefab = Resources.Load<GameObject>("Prefabs/Worker");
            foreach (var npc in npcs)
            {
                var spawnAnchor = SpawnAnchorRegistry.SpawnAnchors[npc.SpawnKey];
                var npcGameObject = Instantiate(prefab, spawnAnchor.Transform.position, spawnAnchor.Transform.rotation);
                var entity = npcGameObject.GetComponent<NpcEntity>();
                entity.Initialise(npc);
                
                npcGameObject.SetActive(true);
            }
        }
    }
}