using AsterGame.Additionally;
using AsterGame.Controller;
using AsterGame.Model.Bullet;
using AsterGame.Model.SpaceShip;
using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Model
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private int _healthPoint;
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private PlayerBullet _bulletPrefab;
        [SerializeField] private Transform _containerPlayerBullets;

        //iterator
        //private List<IShootingMode> _shootingModes;

        private float _offset = -90f;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Ship _ship;
        private InputController _inputController;
        private ObjectInsideCamera _insideCamera;
        private ChangerHealth _changerHealth;
        private PoolForGameObjects<PlayerBullet> _bulletsPool;


        private void Awake()
        {
            gameObject.tag = ConstForGame.PLAYER_TAG;

            if(_camera == null)
                _camera = Camera.main;

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _bulletsPool = new PoolForGameObjects<PlayerBullet>(_bulletPrefab,
                _bulletPrefab.PoolCount, _containerPlayerBullets)
            {
                AutoPoolExpansion = _bulletPrefab.AutoPoolExpansion
            };

            var moveTransform = new AccelerationMove(_rigidbody, _speed, _acceleration);
            var rotation = new RotationShip(transform, _offset);
            var shoot = new ShootShip(_cannonBarrel, _bulletsPool/*, _shootingModes*/);
            _ship = new Ship(moveTransform, rotation, shoot);

            _inputController = new InputController(_ship, _camera);
            _insideCamera = new ObjectInsideCamera(_camera, _rigidbody, _spriteRenderer);
            _changerHealth = new ChangerHealth(_healthPoint);
        }

        //iterator
        //private void Start()
        //{
        //    _shootingModes = new List<IShootingMode>
        //    {
        //        new ShootingMode("Bullet",50,ProjectileType.Bullet),
        //        new ShootingMode("Laser",100,ProjectileType.Laser),
        //    };
        //}

        private void Update() => _inputController.Shoot(_cannonBarrel);

        private void LateUpdate()
        {
            _inputController.Moving();
            _inputController.Rotating(transform.position);
        }

        private void FixedUpdate() => _insideCamera.KeepInsideCamera();


        public void ChangeHealthPlayer(int value)
        {
            bool isAlive = true;
            _changerHealth.ChangeHealth(ref _healthPoint, value, ref isAlive);

            if (isAlive == false)
                gameObject.SetActive(isAlive);

            // messege broker
            //MessageBase.Create(this, ServiceShareData.MSG_DEATH, "Death");
        }
    }
}