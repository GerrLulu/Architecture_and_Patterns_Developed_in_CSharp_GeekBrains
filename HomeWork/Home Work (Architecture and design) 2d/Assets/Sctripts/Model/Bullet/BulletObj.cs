using AsterGame.Additionally;
using System;
using UnityEngine;

namespace AsterGame.Model.Bullet
{
    internal abstract class BulletObj : MonoBehaviour
    {
        [Header("Урон от пули")]
        [SerializeField] private int _damage;

        [Header("Сила выстрела")]
        [SerializeField] private float _forceOfShot;

        [Header("Пул пуль")]
        [SerializeField] private int _poolCount;
        [SerializeField] private bool _autoPoolExpansion;

        private Rigidbody2D _rigidbody;

        public int PoolCount
        {
            get { return _poolCount; }
            protected set { _poolCount = value; }
        }
        public bool AutoPoolExpansion
        {
            get { return _autoPoolExpansion; }
            protected set { _autoPoolExpansion = value; }
        }

        public event Action<int> OnDamagePlayer;
        public event Action<int> OnDamageAsteroid;
        public event Action<int> OnDamageEnemyShip;


        protected virtual void Start() => _rigidbody = GetComponent<Rigidbody2D>();

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == ConstForGame.BACKGROUND_TAG)
                gameObject.SetActive(false);
            return;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider) =>
            Interaction(collider.tag);


        public virtual void Shoot(Vector2 direction) =>
            _rigidbody.AddForce(direction * _forceOfShot, ForceMode2D.Impulse);

        private void Interaction(string nameTag)
        {
            switch (nameTag)
            {
                case ConstForGame.PLAYER_TAG:
                    OnDamagePlayer?.Invoke(_damage);
                    break;
                case ConstForGame.ASTEROID_TAG:
                    OnDamageAsteroid?.Invoke(_damage);
                    break;
                case ConstForGame.ENEMY_SHIP_TAG:
                    OnDamageEnemyShip?.Invoke(_damage);
                    break;
                default:
                    return;
            }

            gameObject.SetActive(false);
        }
    }
}