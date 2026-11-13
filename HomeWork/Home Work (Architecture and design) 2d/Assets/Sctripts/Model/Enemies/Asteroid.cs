using AsterGame.Additionally;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal sealed class Asteroid : Enemy
    {
        protected override void Start()
        {
            gameObject.tag = ConstForGame.ASTEROID_TAG;

            _healthPoint = Random.Range(_healthPoint, Mathf.
                FloorToInt(_healthPoint * Randomizer(_minMultiplier, _maxMultiplier)));
            _collisionDamage = Random.Range(_collisionDamage, Mathf.
                FloorToInt(_collisionDamage * Randomizer(_minMultiplier, _maxMultiplier)));

            base.Start();

            transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
            transform.localScale = new Vector3(transform.localScale.x * Randomizer(_minMultiplier, _maxMultiplier),
                transform.localScale.y * Randomizer(_minMultiplier, _maxMultiplier), 0.0f);

            _rigidbody.mass = _rigidbody.mass * Randomizer(_minMultiplier, _maxMultiplier);
        }


        public override void Move(Vector2 direction)
        {
            var forse = Random.Range(_moveForce, _maxMoveForse);
            _rigidbody.AddForce(direction * forse, ForceMode2D.Impulse);
        }

        //Visitor
        //public void ActivateLoger(ILogerAsteroid value, Vector3 spawnPoint, Quaternion spawnRotation, float size, float hp)
        //{
        //    value.Visit(this, new InfoAsteroid(spawnPoint, spawnRotation, size, hp));
        //}
    }
}