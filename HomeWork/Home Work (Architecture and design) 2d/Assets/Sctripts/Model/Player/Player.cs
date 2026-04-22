using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Model.Player
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
        private Ship _ship;
        private UnputController _unputController;


        private void Awake()
        {
            gameObject.tag = "Player";
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody2D>();

            var moveTransform = new AccelerationMove(_rigidbody, _speed, _acceleration);
            var rotation = new RotationShip(transform, _offset);
            _ship = new Ship(moveTransform, rotation/*, shoot*/);

            _unputController = new UnputController(_ship);

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
            _unputController.Moving();

            var direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _unputController.Rotating(direction);

            //if (Input.GetButtonDown("Fire1"))
            //    _ship.InstantiateBullet(_barrel);
        }


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