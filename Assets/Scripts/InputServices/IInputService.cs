using Assets.Scripts.Infrastructure.DI;
using UnityEngine;

namespace Assets.Scripts.InputServices
{
    public interface IInputService : IService
    {
        bool Tap { get; }
        Vector3 TapPlace { get; }
    }
}