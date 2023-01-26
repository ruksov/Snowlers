using System;

namespace Snowlers.Input
{
    public interface IInputService
    {
        public event Action OnTap;
        public event Action OnTurn;
        public event Action OnSharpTurn;

        public void Enable();
        public void Disable();
    }
}
