using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.Player
{
    internal class RotationShip : IRotation
    {
        private readonly Transform _transform;

        public float Offset { get; protected set; }


        public RotationShip(Transform transform, float offset)
        {
            _transform = transform;
            Offset = offset;
        }


        public void Rotation(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f,0f,angle + Offset);
        }
    }
}