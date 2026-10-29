using AsterGame.Interface;
using UnityEngine;

namespace AsterGame.Model.Enemies
{
    internal sealed class EnemyFactory : IEnemyShipFactory
    {
        //Homework for lesson 3. Factory static method.
        public static Asteroid CreateAsteroid()
        {
            var asteroid = Object.Instantiate(Resources.
                Load<Asteroid>("Prefabs/Enemy/Asteroid"));
            return asteroid;
        }

        public EnemyShip CreateEnemyShip()
        {
            var enemy = Object.Instantiate(Resources.
                Load<EnemyShip>("Prefabs/Enemy/EnemyShip"));
            return enemy;
        }
    }
}