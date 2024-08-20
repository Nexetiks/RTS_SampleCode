using System;

namespace Core
{
    public interface ITickService
    {
        public event Action<float> OnTickChanged;
        public void TickRateUp();
        public void TickRateDown();
        public void Pause();
    }
}
