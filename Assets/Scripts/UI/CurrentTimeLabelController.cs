using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class CurrentTimeLabelController
    {
        [SerializeField] private TextMeshProUGUI currentTimeText;
        
        private ITickService timeService;
        
        public void SetUp(ITickService timeService)
        {
            this.timeService = timeService;
            timeService.OnTickChanged += TimeService_OnTick;
        }
        
        public void Dispose()
        {
            timeService.OnTickChanged -= TimeService_OnTick;
        }
        
        private void TimeService_OnTick(float currentTime)
        {
            currentTimeText.text = currentTime.ToString();
        }
    }
}