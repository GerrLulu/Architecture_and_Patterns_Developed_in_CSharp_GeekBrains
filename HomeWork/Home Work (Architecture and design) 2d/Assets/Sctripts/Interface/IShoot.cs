using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Interface
{
    internal interface IShoot
    {
        void InstantiateBullet(Transform barrel);

        //iterator
        //IShootingMode this[int index] { get; }
        //IEnumerable<IShootingMode> GetMode(ProjectileType index);
    }
}