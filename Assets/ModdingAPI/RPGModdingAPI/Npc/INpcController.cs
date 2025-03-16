using UnityEngine;

namespace ModdingAPI.RPGModdingAPI.Npc
{
    public interface INpcController
    {
        void MoveToDestination(Vector3 destination);
        void StopMoving();
    }
}