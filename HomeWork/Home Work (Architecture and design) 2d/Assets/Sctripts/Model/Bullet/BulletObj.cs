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

        public event Action<int> OnDamage;


        protected virtual void Start() => _rigidbody = GetComponent<Rigidbody2D>();

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == ConstForGame.BACKGROUND_TAG)
                gameObject.SetActive(false);
            return;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            bool isDamageable = collider.tag switch
            {
                ConstForGame.PLAYER_TAG => true,
                ConstForGame.ASTEROID_TAG => true,
                ConstForGame.ENEMY_SHIP_TAG => true,
                _ => false
            };

            if (isDamageable)
            {
                OnDamage?.Invoke(_damage);
                gameObject.SetActive(false);
            }
        }

        
        public virtual void Shoot(Vector2 direction) =>
            _rigidbody.AddForce(direction * _forceOfShot, ForceMode2D.Impulse);
    }
}