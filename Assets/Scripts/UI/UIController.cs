using AYellowpaper;
using Core;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IAgentService> agentService;
        [SerializeField] private InterfaceReference<ITickService> tickService;
        [SerializeField] private AgentAmountLabelController agentAmountLabelController;
        [SerializeField] private AgentsButtons agentsButtons;
        [SerializeField] private TimeButtons uiTimeController;
        [SerializeField] private CurrentTimeLabelController currentTimeLabelController;

        private void OnEnable()
        {
            agentAmountLabelController.SetUp(agentService.Value);
            agentsButtons.SetUp(agentService.Value);
            uiTimeController.SetUp(tickService.Value);
            currentTimeLabelController.SetUp(tickService.Value);
        }

        private void OnDisable()
        {
            agentAmountLabelController.Dispose();
            agentsButtons.Dispose();
            uiTimeController.Dispose();
            currentTimeLabelController.Dispose();
        }
    }
}