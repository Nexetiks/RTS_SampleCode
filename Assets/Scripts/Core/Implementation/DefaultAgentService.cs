using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementation
{
    public class DefaultAgentService : MonoBehaviour, IAgentService
    {
        public event Action<int> OnAgentAmountChanged;
        public event Action<Guid> OnAgentReachedDestination;
        public event Action OnAgentRemoved;
        public event Action OnAgentSpawned;
        public event Action OnAgentRemoveAll;

        private readonly List<Guid> agentIds = new List<Guid>();

        public void RequestAgentSpawn()
        {
            OnAgentSpawned?.Invoke();
            OnAgentAmountChanged?.Invoke(agentIds.Count);
        }

        public void RequestAgentRemove()
        {
            OnAgentRemoved?.Invoke();
            OnAgentAmountChanged?.Invoke(agentIds.Count);
        }

        public void RequestAgentRemoveAll()
        {
            OnAgentRemoveAll?.Invoke();
            OnAgentAmountChanged?.Invoke(0);
        }

        public void RegisterAgent(Guid agentId)
        {
            if (agentIds.Contains(agentId) == false)
            {
                agentIds.Add(agentId);
            }
        }

        public void UnRegisterAgent(Guid agentId)
        {
            if (agentIds.Contains(agentId))
            {
                agentIds.Remove(agentId);
            }
        }

        public void AgentReachedDestination(Guid agentId)
        {
            OnAgentReachedDestination?.Invoke(agentId);
        }
    }
}