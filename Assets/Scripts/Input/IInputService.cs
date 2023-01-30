using System;

namespace Snowlers.Input
{
    public enum EActions
    {
        Menu,
        Gameplay
    }

    public interface IInputService
    {
        public event Action OnTap;
        public event Action OnStartTurn;
        public event Action OnStopTurn;

        public void Enable();
        public void Disable();

        public void Enable(EActions actions);
        public void Disable(EActions actions);
    }
}
