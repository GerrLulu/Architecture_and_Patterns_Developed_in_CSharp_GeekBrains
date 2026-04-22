using UnityEngine;

namespace AsterGame.Interface
{
    public interface IRotation
    {
        float Offset { get; }
        void Rotation(Vector3 direction);
    }
}