using System;

namespace Core
{
    public interface IAgentService
    {
        public event Action<int> OnAgentAmountChanged;
        public event Action<Guid> OnAgentReachedDestination; 
        public event Action OnAgentRemoved;
        public event Action OnAgentSpawned;
        public event Action OnAgentRemoveAll;
        
        public void RequestAgentSpawn();
        public void RequestAgentRemove();
        public void RequestAgentRemoveAll();
        public void RegisterAgent(Guid agentId);
        public void UnRegisterAgent(Guid agentId);
        public void AgentReachedDestination(Guid agentId);
    }
}