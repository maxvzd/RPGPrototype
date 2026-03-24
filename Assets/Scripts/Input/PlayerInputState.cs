using UnityEngine;

namespace Input
{
    public class PlayerInputState : IPlayerInputState
    {
        public Vector2 MovementInput { get; set; }
        public Vector2 MouseInput { get; set; }
    }
}