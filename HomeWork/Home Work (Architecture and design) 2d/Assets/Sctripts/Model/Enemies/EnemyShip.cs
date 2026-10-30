using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal class EnemyShip : Enemy
    {

        protected override void Start()
        {
            base.Start();
        }


        public override void Move(UnityEngine.Vector2 direction)
        {
            throw new NotImplementedException();
        }
    }
}