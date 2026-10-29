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


        private void Start()
        {
            _bullets = GetComponentsInChildren<BulletObj>(true);
            foreach (var bullet in _bullets)
                bullet.OnDamagePlayer += _player.ChangeHealthPlayer;

            _enemies = GetComponentsInChildren<Enemy>(true);
            foreach (var enemy in _enemies)
                enemy.OnCollisionDamage += _player.ChangeHealthPlayer;
        }

        private void OnDisable()
        {
            foreach (var bullet in _bullets)
                bullet.OnDamagePlayer -= _player.ChangeHealthPlayer;

            foreach (var enemy in _enemies)
                enemy.OnCollisionDamage -= _player.ChangeHealthPlayer;
        }
    }
}