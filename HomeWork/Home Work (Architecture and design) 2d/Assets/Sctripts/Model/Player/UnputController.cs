using UnityEngine;

namespace AsterGame.Model.Player
{
    internal sealed class UnputController
    {
        private readonly Ship _ship;

        private readonly Camera _camera;


        public UnputController(Ship ship, Camera camera)
        {
            _ship = ship;
            _camera = camera;
        }


        public void Moving()
        {
            _ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
                _ship.AddAcceleration();
            else
                _ship.RemoveAcceleration();
        }

        public void Rotating(Vector3 position)
        {
            var direction = _camera.ScreenToWorldPoint(Input.mousePosition) - position;
            _ship.Rotation(direction);
        } 
    }
}