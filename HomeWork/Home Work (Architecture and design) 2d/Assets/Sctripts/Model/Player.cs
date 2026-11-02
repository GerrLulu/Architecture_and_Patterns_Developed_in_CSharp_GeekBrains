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
        [Header("Кол-во жизней")]
        [SerializeField] private int _healthPoint;

        [Header("Передвижение")]
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;

        [Header("Стрельба")]
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private PlayerBullet _bulletPrefab;
        [SerializeField] private Transform _containerForBullets;

        //Камера для определение направелния по курсору
        private Camera _camera;
        private float _offset = -90f;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Ship _ship;
        //Класс для считывания ввода
        private InputController _inputController;
        //Класс для того чтобы объект оставался в поле камеры
        private ObjectInsideCamera _insideCamera;
        //Класс для изменения кол-ва жизни
        private ChangerHealth _changerHealth;
        private PoolForGameObjects<PlayerBullet> _bulletsPool;

        //iterator
        //private List<IShootingMode> _shootingModes;


        private void Awake()
        {
            gameObject.tag = ConstForGame.PLAYER_TAG;

            if(_camera == null)
                _camera = Camera.main;

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _bulletsPool = new PoolForGameObjects<PlayerBullet>(_bulletPrefab,
                _bulletPrefab.PoolCount, _containerForBullets)
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