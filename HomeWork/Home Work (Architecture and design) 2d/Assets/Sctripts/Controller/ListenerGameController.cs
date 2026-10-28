using AsterGame.Model;
using AsterGame.Model.Bullet;
using UnityEngine;

namespace AsterGame.Controller
{
    internal sealed class ListenerGameController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private BulletObj[] _bullets;


        private void Start()
        {
            _bullets = GetComponentsInChildren<BulletObj>(true);
            Debug.Log(_bullets.Length);
            foreach (var bullet in _bullets)
            {
                bullet.OnDamagePlayer += _player.ChangeHealthPlayer;
            }
        }

        private void OnDisable()
        {
            foreach (var bullet in _bullets)
            {
                bullet.OnDamagePlayer -= _player.ChangeHealthPlayer;
            }
        }
    }
}