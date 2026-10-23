using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Additionally
{
    internal sealed class PoolForGameObjects<T> where T : MonoBehaviour
    {
        private T _prefabGameObject;
        private List<T> _pool;
        private Transform _container;
        
        public bool autoPoolExpansion { get; set; }


        public PoolForGameObjects(T prefabGameObject, int count, Transform container)
        {
            _prefabGameObject = prefabGameObject;
            _container = container;

            CreatePool(count);
        }


        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
                CreateGameObject();
        }

        private T CreateGameObject(bool isActiveByDefault = false)
        {
            var createdGameObject = UnityEngine.Object.Instantiate(_prefabGameObject, _container);
            createdGameObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdGameObject);
            return createdGameObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach (var poolElement in _pool)
            {
                if (!poolElement.gameObject.activeInHierarchy)
                {
                    element = poolElement;
                    poolElement.gameObject.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (autoPoolExpansion)
                return CreateGameObject(true);

            throw new Exception($"Нет свободных элементов в пуле типа {typeof(T)}");
        }
    }
}