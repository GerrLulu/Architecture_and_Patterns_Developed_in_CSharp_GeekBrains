using AsterGame.Additionally;
using AsterGame.Controller;
using System;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal abstract class Enemy : MonoBehaviour
    {
        [Header("Множитель")]
        [SerializeField] protected float _minMultiplier;
        [SerializeField] protected float _maxMultiplier;

        [Header("Кол-во очков жизни")]
        [SerializeField] protected int _healthPoint;

        [Header("Движение")]
        [SerializeField] protected float _moveForce;

        [Header("Урон от прикосновения")]
        [SerializeField] protected int _collisionDamage;

        [Header("Пул врагов")]
        [SerializeField] private int _poolCount;
        [SerializeField] private bool _autoPoolExpansion;

        protected float _maxMoveForse;
        private Camera _camera;
        protected Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private ObjectInsideCamera _insideCamera;
        private ChangerHealth _changerHealth;

        public int PoolCount
        {
            get {  return _poolCount; }
            protected set { _poolCount = value; }
        }
        public bool AutoPoolExpansion
        {
            get { return _autoPoolExpansion; }
            protected set { _autoPoolExpansion = value; }
        }

        public event Action<int> OnColliderDamage;


        protected virtual void Start ()
        {
            _camera = Camera.main;

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _insideCamera = new ObjectInsideCamera(_camera, _rigidbody, _spriteRenderer);
            _changerHealth = new ChangerHealth(_healthPoint);

            _maxMoveForse = _moveForce * Randomizer(_minMultiplier, _maxMultiplier);
        }

        private void FixedUpdate() => _insideCamera.KeepInsideCamera();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            bool isDamageable = collider.tag switch
            {
                ConstForGame.PLAYER_TAG => true,
                ConstForGame.ASTEROID_TAG => true,
                ConstForGame.ENEMY_SHIP_TAG => true,
                _ => false
            };

            if (isDamageable)
                OnColliderDamage?.Invoke(_collisionDamage);
        }


        public abstract void Move(Vector2 direction);

        public virtual void TakeDamage(int value)
        {
            bool isAlive = gameObject.activeSelf;
            _changerHealth.ChangeHealth(ref _healthPoint, value, ref isAlive);

            if (isAlive == false)
                gameObject.SetActive(false);

            Debug.Log("Name:" + gameObject + "HP:" + _healthPoint);
        }

        protected float Randomizer(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}