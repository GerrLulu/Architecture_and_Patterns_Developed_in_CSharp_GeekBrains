using AsterGame.Controller;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private int _healthPoint;

        protected Rigidbody2D _rigidbody;
        protected ChangerHealth _changerHealth;

        


        private void Start ()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _changerHealth = new ChangerHealth(_healthPoint);
        }


        protected abstract void Move();

        protected void DamageEnemy(int value)
        {
            bool isAlive = gameObject.activeSelf;
            _changerHealth.ChangeHealth(ref _healthPoint, value, ref isAlive);

            if (isAlive == false)
                gameObject.SetActive(false);
        }
    }
}