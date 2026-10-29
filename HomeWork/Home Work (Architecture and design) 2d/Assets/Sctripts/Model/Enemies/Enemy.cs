using AsterGame.Additionally;
using AsterGame.Controller;
using System;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private int _healthPoint;
        [SerializeField] private int _collisionDamage;

        private Camera _camera;
        protected Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private ObjectInsideCamera _insideCamera;
        private ChangerHealth _changerHealth;

        public event Action<int> OnCollisionDamage;


        protected virtual void Start ()
        {
            _camera = Camera.main;

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _insideCamera = new ObjectInsideCamera(_camera, _rigidbody, _spriteRenderer);
            _changerHealth = new ChangerHealth(_healthPoint);
        }

        private void FixedUpdate() => _insideCamera.KeepInsideCamera();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == ConstForGame.PLAYER_TAG)
                OnCollisionDamage?.Invoke(_collisionDamage);
        }


        public abstract void Move(Vector2 direction);

        protected virtual void TakeDamage(int value)
        {
            bool isAlive = gameObject.activeSelf;
            _changerHealth.ChangeHealth(ref _healthPoint, value, ref isAlive);

            if (isAlive == false)
                gameObject.SetActive(false);
        }
    }
}