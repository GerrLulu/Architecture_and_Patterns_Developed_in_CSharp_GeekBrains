using UnityEngine;

namespace AsterGame.Model.Player
{
    internal class AccelerationMove : MoveTransform
    {
        private readonly float _acceleration;


        public AccelerationMove(Rigidbody2D rigidbody, float speed, float acceleration) : base(rigidbody, speed)
        {
            _acceleration = acceleration;
        }


        public void AddAcceleration() => Speed += _acceleration;

        public void RemoveAcceleration() => Speed -= _acceleration;
    }
}