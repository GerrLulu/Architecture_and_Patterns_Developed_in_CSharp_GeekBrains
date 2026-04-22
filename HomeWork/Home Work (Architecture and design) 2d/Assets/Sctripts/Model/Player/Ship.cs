using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.Player
{
    internal sealed class Ship : IMove, IRotation/*, IShoot*/
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        //private readonly IShoot _shootImplementation;

        public float Speed => _moveImplementation.Speed;
        public float Offset => _rotationImplementation.Offset;


        public Ship(IMove moveImplementation, IRotation rotationImplementation/*, IShoot shootImplementation*/)
        {
            _moveImplementation = moveImplementation;
            _rotationImplementation = rotationImplementation;
            //_shootImplementation = shootImplementation;
        }


        public void Move(float horizontal, float vertical) => _moveImplementation.Move(horizontal, vertical);

        public void Rotation(Vector3 direction) => _rotationImplementation.Rotation(direction);

        //public void InstantiateBullet(Transform barrel) => _shootImplementation.InstantiateBullet(barrel);

        public void AddAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
                accelerationMove.AddAcceleration();
        }

        public void RemoveAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
                accelerationMove.RemoveAcceleration();
        }
    }
}