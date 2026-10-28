using AsterGame.Additionally;
using System;
using UnityEngine;

namespace AsterGame.Model.Bullet
{
    internal abstract class BulletObj : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _forceOfShot;
        [SerializeField] private int _poolCount;
        [SerializeField] private bool _autoPoolExpansion;

        private Rigidbody2D _rigidbody;

        private string _nameTag;

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


        protected virtual void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == ConstForGame.BACKGROUND_TAG)
                gameObject.SetActive(false);
            return;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _nameTag = collision.tag;
            Interaction();
        }

        
        public virtual void Shoot(Vector2 direction) => _rigidbody.AddForce(direction * _forceOfShot);

        protected virtual void Interaction()
        {
            switch (_nameTag)
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