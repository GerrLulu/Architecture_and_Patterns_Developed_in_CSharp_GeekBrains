using AsterGame.Additionally;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsterGame.Model.Bullet
{
    internal class Bullet : MonoBehaviour
    {
        [SerializeField] protected float ForceOfShot;
        
        private Rigidbody2D _rigidbody;

        public string NameTag {  get; private set; }


        public void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        
        public void Shoot(Vector2 direction) => _rigidbody.AddForce(direction * ForceOfShot);

        protected void Interaction()
        {
            switch (NameTag)
            {
                case ConstForGame.PLAYER_TAG:
                    break;
                case ConstForGame.ASTEROID_TAG:
                    break;
                case ConstForGame.ENEMY_SHIP:
                    break;
                default:
                    return;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == ConstForGame.BACKGROUND)
                gameObject.SetActive(false);
            return;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            NameTag = collision.tag;
            Interaction();
        }
    }
}