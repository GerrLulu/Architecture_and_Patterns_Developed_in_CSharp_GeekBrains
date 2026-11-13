using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.SpaceShip
{
    internal class RotationShip : IRotation
    {
        private readonly Transform _transform;
        private float _offset;

        public float Offset { get { return _offset; } }


        public RotationShip(Transform transform, float offset)
        {
            _transform = transform;
            _offset = offset;
        }


        public void Rotation(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0f, 0f, angle + _offset);
        }
    }
}