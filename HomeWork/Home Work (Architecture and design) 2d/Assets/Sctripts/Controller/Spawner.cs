using AsterGame.Additionally;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AsterGame.Controller
{
    internal sealed class Spawner<T> where T : MonoBehaviour
    {
        private float _spawnRate;
        private float _spawnAmount; // at one time/за раз
        private Camera _camera;
        private T _prefab;
        private PoolForGameObjects<T> _pool;
        private int _poolCount;
        private bool _autoPoolExpansion;

        private float _minSize;
        private float _maxSize;

        private float _spawnDistance;
        private float _trajectoryVariance;


        public Spawner(float spawnRate, float spawnAmount, Camera camera,
            T prefab, PoolForGameObjects<T> pool, int poolCount,
            bool autoPoolExpansion)
        {
            _spawnRate = spawnRate;
            _spawnAmount = spawnAmount;
            _camera = camera;
            _prefab = prefab;
            _pool = pool;
            _poolCount = poolCount;
            _autoPoolExpansion = autoPoolExpansion;
        }

        public Spawner(float spawnRate, float spawnAmount, Camera camera,
            T prefab, PoolForGameObjects<T> pool, int poolCount,
            bool autoPoolExpansion, float minSize, float maxSize)
        {
            _spawnRate = spawnRate;
            _spawnAmount = spawnAmount;
            _camera = camera;
            _prefab = prefab;
            _pool = pool;
            _poolCount = poolCount;
            _autoPoolExpansion = autoPoolExpansion;
            _minSize = minSize;
            _maxSize = maxSize;
        }


        private void Spawn()
        {
            Vector3 spawnDirection;
            Vector3 spawnPoint;
        }
    }
}