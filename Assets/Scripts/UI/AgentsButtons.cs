using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class AgentsButtons
    {
        [SerializeField] private Button spawnButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button removeAllButton;

        private IAgentService agentService;

        public void SetUp(IAgentService agentService)
        {
            this.agentService = agentService;
            spawnButton.onClick.AddListener(SpawnButton_OnClick);
            removeButton.onClick.AddListener(RemoveButton_OnClick);
            removeAllButton.onClick.AddListener(agentService.RequestAgentRemoveAll);
        }

        public void Dispose()
        {
            spawnButton.onClick.RemoveListener(SpawnButton_OnClick);
            removeButton.onClick.RemoveListener(RemoveButton_OnClick);
            removeAllButton.onClick.RemoveListener(RemoveAllButton_OnClick);
        }

        private void SpawnButton_OnClick()
        {
            agentService.RequestAgentSpawn();
        }

        private void RemoveButton_OnClick()
        {
            agentService.RequestAgentRemove();
        }

        private void RemoveAllButton_OnClick()
        {
            agentService.RequestAgentRemoveAll();
        }
    }
}