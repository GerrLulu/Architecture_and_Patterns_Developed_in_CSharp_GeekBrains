using AsterGame.Model.Bullet;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal class EnemyShip : Enemy
    {
        [Header("Стрельба")]
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private EnemyBullet _bulletPrefab;
        [SerializeField] private Transform _containerEnemyBullets;
        [SerializeField] private float _fireInterval;

        [Header("Движение")]
        [SerializeField] private float _stopTreshold;
        [SerializeField] private float _maxMoveTime;
        [SerializeField] private float _maxStopTime;

        private Transform _playeTarget;



        protected override void Start()
        {
            base.Start();

            if (_playeTarget == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                    _playeTarget = player.transform;
            }
        }


        public override void Move(UnityEngine.Vector2 direction)
        {
            throw new NotImplementedException();
        }
    }
}