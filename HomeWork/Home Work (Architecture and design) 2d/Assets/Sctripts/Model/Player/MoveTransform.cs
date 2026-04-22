using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.Player
{
    internal class MoveTransform : IMove
    {
        private Rigidbody2D _rigidbody;

        public float Speed { get; protected set; }


        public MoveTransform(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            Speed = speed;
        }


        public void Move(float horizontal, float vertical)
        {
            Vector2 movement = new Vector2(horizontal, vertical);
            _rigidbody.AddForce(movement * Speed * Time.deltaTime);
        }
    }
}