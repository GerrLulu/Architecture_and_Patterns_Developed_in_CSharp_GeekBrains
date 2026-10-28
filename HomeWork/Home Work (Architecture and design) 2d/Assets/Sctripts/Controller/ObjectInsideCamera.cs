using UnityEngine;

namespace AsterGame.Controller
{
    internal sealed class ObjectInsideCamera
    {
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;


        public ObjectInsideCamera(Camera camera, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer)
        {
            _camera = camera;
            _rigidbody = rigidbody;
            _spriteRenderer = spriteRenderer;
        }


        public void KeepInsideCamera()
        {
            float halfHeight = _camera.orthographicSize;
            float halfWidth = halfHeight * _camera.aspect;
            Vector3 cameraPosition = _camera.transform.position;

            float marginX = _spriteRenderer.bounds.extents.x;
            float marginY = _spriteRenderer.bounds.extents.y;

            float minX = cameraPosition.x - halfWidth + marginX;
            float maxX = cameraPosition.x + halfWidth - marginX;
            float minY = cameraPosition.y - halfHeight + marginY;
            float maxY = cameraPosition.y + halfHeight - marginY;

            Vector2 positionRb = _rigidbody.position;
            Vector2 velocityRb = _rigidbody.linearVelocity; // поменялось с velocity, т.к. устарела

            if (positionRb.x  < minX)
                positionRb.x = maxX;

            if (positionRb.x > maxX)
                positionRb.x = minX;

            if (positionRb.y < minY)
                positionRb.y = maxY;

            if (positionRb.y > maxY)
                positionRb.y = minY;

            _rigidbody.position = positionRb;
            _rigidbody.linearVelocity = velocityRb;
        }
    }
}