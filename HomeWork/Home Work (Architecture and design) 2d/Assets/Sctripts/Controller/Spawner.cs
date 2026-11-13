using AsterGame.Additionally;
using AsterGame.Interface;
using AsterGame.Model.Enemies;
using UnityEngine;

namespace AsterGame.Controller
{
    internal sealed class Spawner : MonoBehaviour
    {
        [Header("Отступы")]
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _marginOffset;

        [Header("Префабы")]
        [SerializeField] private Asteroid _prefabAsteroid;
        [SerializeField] private EnemyShip _prefabEnemyShip;

        [Header("Контейнеры для пулов")]
        [SerializeField] private Transform _conteinerAsteroids;
        [SerializeField] private Transform _conteinerEnemyShips;
        [SerializeField] private Transform _conteinerEnemyBullets;

        [Header("Лимит врагов")]
        [SerializeField] private int _maxAsteroids;
        [SerializeField] private int _maxEnemyShips;

        [Header("Интервал спавна")]
        [SerializeField] private float _asteroidInterval;
        [SerializeField] private float _enemyShipInterval;

        private Camera _camera;
        private float _halfHeight;
        private float _halfWeidth;
        private Vector3 _camPosition;

        private PoolForGameObjects<Asteroid> _asteroidsPool;
        private PoolForGameObjects<EnemyShip> _enemyShipsPool;
        private int _activeAsteroid = 0;
        private int _activeEnemyShips = 0;
        private float _asteroidTimer = 1f;
        private float _enemyShipTimer = 2f;


        private void Awake()
        {
            _camera = Camera.main;
            _halfHeight = _camera.orthographicSize;
            _halfWeidth = _halfHeight * _camera.aspect;
            _camPosition = _camera.transform.position;


            _asteroidsPool = new PoolForGameObjects<Asteroid>(_prefabAsteroid,
                _prefabAsteroid.PoolCount, _conteinerAsteroids)
            {
                AutoPoolExpansion = _prefabAsteroid.AutoPoolExpansion
            };

            _prefabEnemyShip.ConteinerEnemyBullets = _conteinerEnemyBullets;
            _enemyShipsPool = new PoolForGameObjects<EnemyShip>(_prefabEnemyShip,
                _prefabEnemyShip.PoolCount, _conteinerEnemyShips)
            {
                AutoPoolExpansion = _prefabEnemyShip.AutoPoolExpansion
            };

        }

        private void Start()
        {





        }

        private void Update()
        {
            _asteroidTimer -= Time.deltaTime;
            if (_asteroidTimer <= 0)
            {
                TrySpawnAsteroid();
                _asteroidTimer = _asteroidInterval;
                Debug.Log("Spawn asteroid");
            }

            _enemyShipTimer -= Time.deltaTime;
            if ( _enemyShipTimer <= 0)
            {
                TrySpawnEnemyShip();
                _enemyShipTimer = _enemyShipInterval;
                Debug.Log("Spawn Enemy ship");
            }
        }


        private void TrySpawnAsteroid()
        {
            if (_activeAsteroid >= _maxAsteroids)
                return;

            Vector2 spawnPos = GetRandomSpawnPosition();
            Vector2 direction = GetDirecrionInside(spawnPos);

            var asteroid = _asteroidsPool.GetFreeElement();
            asteroid.transform.position = spawnPos;
            Debug.Log(_activeAsteroid);
            asteroid.Move(direction);

            _activeAsteroid++;
        }

        private void TrySpawnEnemyShip()
        {
            if (_activeEnemyShips >= _maxEnemyShips)
                return;

            Vector2 spawnPos = GetRandomSpawnPosition();
            Vector2 direction = GetDirecrionInside(spawnPos);

            var enemyShip = _enemyShipsPool.GetFreeElement();
            enemyShip.transform.position = spawnPos;
            enemyShip.Move(direction);

            _activeEnemyShips++;
        }

        public void EnemyCounting(string gameObjectTag)
        {
            if (gameObjectTag == ConstForGame.ASTEROID_TAG)
                _activeAsteroid--;
            else if (gameObjectTag == ConstForGame.ENEMY_SHIP_TAG)
                _activeEnemyShips--;

            return;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            int side = UnityEngine.Random.Range(0, 4);

            float x = 0f, y = 0f;

            switch (side)
            {
                case 0:
                    x = _camPosition.x - _halfWeidth - _spawnOffset;
                    y = _camPosition.y + UnityEngine.Random.Range(-_halfHeight, _halfHeight);
                    break;
                case 1:
                    x = _camPosition.x + _halfWeidth + _spawnOffset;
                    y = _camPosition.y + UnityEngine.Random.Range(-_halfHeight, _halfHeight);
                    break;
                case 2:
                    x = _camPosition.x + UnityEngine.Random.Range(-_halfWeidth, _halfWeidth);
                    y = _camPosition.y - _halfHeight - _spawnOffset;
                    break;
                case 3:
                    x = _camPosition.x + UnityEngine.Random.Range(-_halfWeidth, _halfWeidth);
                    y = _camPosition.y + _halfHeight + _spawnOffset;
                    break;
            }

            return new Vector2(x, y);
        }

        private Vector2 GetDirecrionInside(Vector2 spawnPos)
        {
            Vector2 target = new Vector2(_camPosition.x +
                UnityEngine.Random.Range(-_halfWeidth * _marginOffset, _halfWeidth * _marginOffset),
                _camPosition.y +
                UnityEngine.Random.Range(-_halfHeight * _marginOffset, _halfHeight * _marginOffset));

            return (target - spawnPos).normalized;
        }
    }
}