using AsterGame.Additionally;
using AsterGame.Model.Bullet;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal class EnemyShip : Enemy
    {
        [Header("Стрельба")]
        [SerializeField] private float _shootTimer;
        [SerializeField] private float _fireInterval;
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private EnemyBullet _bulletPrefab;
        [SerializeField] private Transform _containerEnemyBullets;

        [Header("Движение")]
        [SerializeField] private float _stopTreshold;
        [SerializeField] private float _pauseAfterStop;

        private float _pauseTimer;
        private float _offset = -90f;
        private Transform _playeTarget;
        private PoolForGameObjects<EnemyBullet> _bulletsPool;

        public Transform ConteinerEnemyBullets {  get; set; }


        protected override void Start()
        {
            gameObject.tag = ConstForGame.ENEMY_SHIP_TAG;

            base.Start();

            if (_playeTarget == null)
            {
                var player = GameObject.FindGameObjectWithTag(ConstForGame.PLAYER_TAG);
                if (player != null)
                    _playeTarget = player.transform;
            }

            _bulletsPool = new PoolForGameObjects<EnemyBullet>(_bulletPrefab,
                _bulletPrefab.PoolCount, _containerEnemyBullets)
            {
                AutoPoolExpansion = _bulletPrefab.AutoPoolExpansion
            };
        }

        private void Update()
        {
            _shootTimer -= Time.deltaTime;
            if (_shootTimer <= 0)
            {
                Shoot();
                _shootTimer = _fireInterval;
            }
        }

        private void LateUpdate()
        {
            Rotation();

            if (_rigidbody.linearVelocity.sqrMagnitude < _stopTreshold * _stopTreshold)
            {
                _rigidbody.linearVelocity = Vector2.zero;

                _pauseAfterStop -= Time.deltaTime;
                if ( _pauseAfterStop <= 0)
                    DeterminingDirection();
            }
        }


        private void DeterminingDirection()
        {
            float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

            Move(direction);
        }

        public override void Move(Vector2 direction)
        {
            var forse = Random.Range(_moveForce, _maxMoveForse);
            _rigidbody.AddForce(direction * forse, ForceMode2D.Impulse);

            _pauseTimer = _pauseAfterStop;
        }

        private void Rotation()
        {
            Vector2 direction = (_playeTarget.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + _offset);
        }

        private void Shoot()
        {
            var bulletsMagazine = _bulletsPool.GetFreeElement();
            bulletsMagazine.transform.position = _cannonBarrel.position;
            bulletsMagazine.transform.rotation = _cannonBarrel.rotation;
            bulletsMagazine.Shoot(_cannonBarrel.up);
        }
    }
}