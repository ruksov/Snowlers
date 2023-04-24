using System;

namespace Snowlers.Infrastructure.Services.Input
{
    public interface IInputService
    {
        public event Action OnTap;

        public void Enable();
        public void Disable();
    }
}
