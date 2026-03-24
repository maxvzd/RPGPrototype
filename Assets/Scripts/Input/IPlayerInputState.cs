using UnityEngine;

namespace Input
{

    public interface IPlayerInputState
    {
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
    }
}