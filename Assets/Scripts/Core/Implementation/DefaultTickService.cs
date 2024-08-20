using System;
using UnityEngine;

namespace Core.Implementation
{
    public class DefaultTickService : MonoBehaviour, ITickService
    {
        private const float TICK_RATE_STEP = 0.1f;
        
        public event Action<float> OnTickChanged;

        private float tickRate = 1;
        
        public void TickRateUp()
        {
            SetTickRate(tickRate + TICK_RATE_STEP);
        }
        
        public void TickRateDown()
        {
            SetTickRate(tickRate - TICK_RATE_STEP);
        }
        public void Pause()
        {
            SetTickRate(0);
        }

        private void SetTickRate(float tickRate)
        {
            if (tickRate < 0f)
            {
                tickRate = 0f;
            }
            
            tickRate = RoundToOneDecimalPlace(tickRate);

            this.tickRate = tickRate;
            Time.timeScale = tickRate;
            OnTickChanged?.Invoke(tickRate);
        }

        private float RoundToOneDecimalPlace(float value)
        {
            return Mathf.Round(value * 10f) / 10f;
        }
    }
}