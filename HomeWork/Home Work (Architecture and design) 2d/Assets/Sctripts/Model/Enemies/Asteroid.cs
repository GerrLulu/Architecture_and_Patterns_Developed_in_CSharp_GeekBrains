using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal class Asteroid : Enemy
    {
        [SerializeField] private float _minForse;

        private float _maxForse;


        protected override void Start()
        {
            _healthPoint = Random.Range(_minHealthPoint, Mathf.
                FloorToInt(_minHealthPoint * Randomizer(_minMultiplier, _maxMultiplier)));
            _collisionDamage = Random.Range(_minCollisionDamage, Mathf.
                FloorToInt(_minCollisionDamage * Randomizer(_minMultiplier, _maxMultiplier)));

            _maxForse = _minForse * Randomizer(_minMultiplier, _maxMultiplier);

            base.Start();

            transform.eulerAngles = new Vector3(0.0f, 0.0f,
                Random.value * 360.0f);
            transform.localScale = new Vector3(transform.localScale.x *
                Randomizer(_minMultiplier, _maxMultiplier), transform.localScale.y *
                Randomizer(_minMultiplier, _maxMultiplier), 0.0f);

            _rigidbody.mass = _rigidbody.mass * Randomizer(_minMultiplier, _maxMultiplier);
        }


        public override void Move(Vector2 direction)
        {
            var forse = Random.Range(_minForse, _maxForse);
            _rigidbody.AddForce(direction * forse);
        }

        //Visitor
        //public void ActivateLoger(ILogerAsteroid value, Vector3 spawnPoint, Quaternion spawnRotation, float size, float hp)
        //{
        //    value.Visit(this, new InfoAsteroid(spawnPoint, spawnRotation, size, hp));
        //}
    }
}