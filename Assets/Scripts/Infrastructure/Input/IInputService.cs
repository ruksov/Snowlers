using System;

namespace Snowlers.Infrastructure.Input
{
    public interface IInputService
    {
        public event Action OnTap;

        public void Enable();
        public void Disable();
    }
}
