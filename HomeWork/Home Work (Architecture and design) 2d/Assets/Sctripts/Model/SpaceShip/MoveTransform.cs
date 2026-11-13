using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.SpaceShip
{
    internal class MoveTransform : IMove
    {
        private Rigidbody2D _rigidbody;
        protected float _speed;

        public float Speed { get { return _speed; } }


        public MoveTransform(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }


        public void Move(float horizontal, float vertical)
        {
            Vector2 movement = new Vector2(horizontal, vertical);
            _rigidbody.AddForce(movement * _speed * Time.deltaTime);
        }
    }
}