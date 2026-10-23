using AsterGame.Additionally;
using AsterGame.Controller;
using AsterGame.Model.SpaceShip;
using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Model
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _offset;
        [SerializeField] private float _hp;
        [SerializeField] private Transform _barrel;
        //[SerializeField] private PlayerBullets _bulletPrefab;

        //iterator
        //private List<IShootingMode> _shootingModes;
        //private PoolForGameObjects<PlayerBullets> _pool;

        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Ship _ship;
        private InputController _inputController;
        private InsideCamera _insideCamera;


        private void Awake()
        {
            gameObject.tag = ConstForGame.PLAYER_TAG;

            if(_camera == null)
                _camera = Camera.main;

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            var moveTransform = new AccelerationMove(_rigidbody, _speed, _acceleration);
            var rotation = new RotationShip(transform, _offset);
            _ship = new Ship(moveTransform, rotation/*, shoot*/);

            _inputController = new InputController(_ship, _camera);
            _insideCamera = new InsideCamera(_camera, _rigidbody, _spriteRenderer);

            //_pool = new PoolForGameObjects<PlayerBullets>(_bulletPrefab, _bulletPrefab.PoolCount, transform);
            //_pool.autoPoolExpansion = _bulletPrefab.AutoPoolExpansion;

            //var shoot = new ShootShip(_barrel, _pool, _shootingModes);
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

        private void Update()
        {
            //if (Input.GetButtonDown("Fire1"))
            //    _ship.InstantiateBullet(_barrel);
        }

        private void LateUpdate()
        {
            _inputController.Moving();
            _inputController.Rotating(transform.position);
        }

        private void FixedUpdate() => _insideCamera.KeepInsideCamera();


        public void MinusPlayerHP(float value)
        {
            if (_hp > 0)
            {
                _hp -= value;
            }
            else
            {
                //messege broker
                //MessageBase.Create(this, ServiceShareData.MSG_DEATH, "Death");
                Destroy(gameObject);
            }
        }
    }
}