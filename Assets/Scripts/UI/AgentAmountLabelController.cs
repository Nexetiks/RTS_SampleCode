using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class AgentAmountLabelController
    {
        [SerializeField] private TextMeshProUGUI agentAmountText;
        [SerializeField] private TextMeshProUGUI agentReachedText;

        private IAgentService agentService;
        private ChatPanel chatPanel;

        public void SetUp(IAgentService agentService)
        {
            this.agentService = agentService;
            chatPanel = new ChatPanel(agentReachedText);
            agentService.OnAgentAmountChanged += AgentService_OnAgentAmountChanged;
            agentService.OnAgentReachedDestination += AgentService_OnAgentReachedDestination;
        }

        public void Dispose()
        {
            agentService.OnAgentAmountChanged -= AgentService_OnAgentAmountChanged;
            agentService.OnAgentReachedDestination -= AgentService_OnAgentReachedDestination;
        }

        private void AgentService_OnAgentAmountChanged(int agentAmount)
        {
            agentAmountText.text = agentAmount.ToString();
        }

        private void AgentService_OnAgentReachedDestination(Guid agentId)
        {
            chatPanel.AddMessage($"Agent {agentId} reached destination");
        }
    }
}