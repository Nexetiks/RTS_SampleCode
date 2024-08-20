using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class TimeButtons
    {
        [SerializeField] private Button speedUpButton;
        [SerializeField] private Button speedDownButton;
        [SerializeField] private Button pauseButton;
        
        private ITickService timeService;
        
        public void SetUp(ITickService timeService)
        {
            this.timeService = timeService;
            speedUpButton.onClick.AddListener(SpeedUpButton_OnClick);
            speedDownButton.onClick.AddListener(SpeedDownButton_OnClick);
            pauseButton.onClick.AddListener(PauseButton_OnClick);
        }
        
        public void Dispose()
        {
            speedUpButton.onClick.RemoveListener(SpeedUpButton_OnClick);
            speedDownButton.onClick.RemoveListener(SpeedDownButton_OnClick);
            pauseButton.onClick.RemoveListener(PauseButton_OnClick);
        }
        
        private void SpeedUpButton_OnClick()
        {
            timeService.TickRateUp();
        }
        
        private void SpeedDownButton_OnClick()
        {
            timeService.TickRateDown();
        }
        
        private void PauseButton_OnClick()
        {
            timeService.Pause();
        }
    }
}