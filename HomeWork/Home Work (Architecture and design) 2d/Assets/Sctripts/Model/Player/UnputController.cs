using UnityEngine;

namespace AsterGame.Model.Player
{
    internal sealed class UnputController
    {
        private readonly Ship _ship;


        public UnputController(Ship ship)
        {
            _ship = ship;
        }


        public void Moving()
        {
            _ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
                _ship.AddAcceleration();
            else
                _ship.RemoveAcceleration();
        }

        public void Rotating( Vector3 dir) => _ship.Rotation(dir);
    }
}