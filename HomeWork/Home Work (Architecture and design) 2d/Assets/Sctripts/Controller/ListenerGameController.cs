using AsterGame.Model;
using AsterGame.Model.Bullet;
using AsterGame.Model.Enemies;
using UnityEngine;

namespace AsterGame.Controller
{
    internal sealed class ListenerGameController : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private BulletObj[] _bullets;
        private Enemy[] _enemies;
        private Spawner _spawner;


        private void Start()
        {
            _bullets = GetComponentsInChildren<BulletObj>(true);
            _enemies = GetComponentsInChildren<Enemy>(true);

            _spawner = GetComponent<Spawner>();

            foreach (var bullet in _bullets)
            {
                bullet.OnDamagePlayer += _player.ChangeHealthPlayer;

                foreach (Asteroid asteroid in _enemies)
                    bullet.OnDamageAsteroid += asteroid.TakeDamage;

                foreach (EnemyShip ship in _enemies)
                    bullet.OnDamageEnemyShip += ship.TakeDamage;
            }

            foreach (var enemy in _enemies)
            {
                enemy.OnColliderDamagePlayer += _player.ChangeHealthPlayer;

                if (enemy is Asteroid)
                    enemy.OnColliderDamageAsteroid += enemy.TakeDamage;

                if (enemy is EnemyShip)
                    enemy.OnColliderDamageEnemyShip += enemy.TakeDamage;

                enemy.OnCounting += _spawner.EnemyCounting;
            }
        }

        private void OnDisable()
        {
            foreach (var bullet in _bullets)
            {
                bullet.OnDamagePlayer -= _player.ChangeHealthPlayer;

                foreach (Asteroid asteroid in _enemies)
                    bullet.OnDamageAsteroid -= asteroid.TakeDamage;

                foreach (EnemyShip ship in _enemies)
                    bullet.OnDamageEnemyShip -= ship.TakeDamage;
            }

            foreach (var enemy in _enemies)
            {
                enemy.OnColliderDamagePlayer -= _player.ChangeHealthPlayer;

                if (enemy is Asteroid)
                    enemy.OnColliderDamageAsteroid -= enemy.TakeDamage;

                if (enemy is EnemyShip)
                    enemy.OnColliderDamageEnemyShip -= enemy.TakeDamage;

                enemy.OnCounting -= _spawner.EnemyCounting;
            }
        }
    }
}