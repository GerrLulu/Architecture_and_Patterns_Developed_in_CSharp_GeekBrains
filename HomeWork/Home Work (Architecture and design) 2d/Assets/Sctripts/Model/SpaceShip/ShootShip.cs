using AsterGame.Additionally;
using AsterGame.Interface;
using AsterGame.Model.Bullet;
using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Model.SpaceShip
{
    internal sealed class ShootShip : IShoot
    {
        private readonly Transform _cannonBarrel;
        private readonly PoolForGameObjects<PlayerBullet> _bulletsPool;
        //iterator
        //private readonly List<IShootingMode> _shootingModes;


        public ShootShip(Transform cannonBarrel,
            PoolForGameObjects<PlayerBullet> bulletsPool/*,
            List<IShootingMode> shootingModes*/)
        {
            _cannonBarrel = cannonBarrel;
            _bulletsPool = bulletsPool;
            //_shootingModes = shootingModes;
        }


        //iterator
        //public IShootingMode this[int index] => _shootingModes[index];

        //public IEnumerable<IShootingMode> GetMode(ProjectileType index)
        //{
        //    foreach (IShootingMode shootingMode in _shootingModes)
        //    {
        //        if (shootingMode.ProjectileType == index)
        //            yield return shootingMode;
        //    }
        //}


        public void InstantiateBullet(Transform cannonBarrel)
        {
            cannonBarrel = _cannonBarrel;
            var bulletsMagazine = _bulletsPool.GetFreeElement();
            bulletsMagazine.transform.position = cannonBarrel.position;
            bulletsMagazine.transform.rotation = cannonBarrel.rotation;
            bulletsMagazine.Shoot(cannonBarrel.up);
        }
    }
}