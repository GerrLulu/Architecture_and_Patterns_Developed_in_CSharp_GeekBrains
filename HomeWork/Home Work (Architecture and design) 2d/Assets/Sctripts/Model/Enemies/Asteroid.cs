using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal class Asteroid : Enemy
    {
        [SerializeField] private float _maxForse;
        [SerializeField] private float _minForse;

        public float Size;


        protected override void Start()
        {
            base.Start();

            transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
            transform.localScale = Vector3.one * Size;

            _rigidbody.mass = Size;
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