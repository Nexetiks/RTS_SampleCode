using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper;
using Core;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Agent
{
    public class AgentManagerService : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IAgentService> agentService;
        [SerializeField] private InterfaceReference<ITickService> tickService;
        [SerializeField] private GameObject agentPrefab;
        [SerializeField] private int initialPoolSize = 10;

        private ObjectPool<Agent> agentPool;
        private readonly Dictionary<Guid, Agent> activeAgents = new Dictionary<Guid, Agent>();

        private void Awake()
        {
            agentPool = new ObjectPool<Agent>(
                CreateAgent,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyAgent,
                false,
                initialPoolSize,
                initialPoolSize
            );

            agentService.Value.OnAgentRemoved += AgentService_OnAgentRemovedHandler;
            agentService.Value.OnAgentSpawned += AgentService_OnAgentSpawnedHandler;
            agentService.Value.OnAgentRemoveAll += AgentService_OnAgentRemoveAllHandler;
            tickService.Value.OnTickChanged += TickService_OnTickChanged;
        }

        private void OnDestroy()
        {
            agentService.Value.OnAgentRemoved -= AgentService_OnAgentRemovedHandler;
            agentService.Value.OnAgentSpawned -= AgentService_OnAgentSpawnedHandler;
            agentService.Value.OnAgentRemoveAll -= AgentService_OnAgentRemoveAllHandler;
            tickService.Value.OnTickChanged -= TickService_OnTickChanged;
        }

        private Agent CreateAgent()
        {
            GameObject agentObject = Instantiate(agentPrefab);
            Agent agent = agentObject.GetComponent<Agent>();

            return agent;
        }

        private void OnTakeFromPool(Agent agent)
        {
            agent.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(Agent agent)
        {
            agent.gameObject.SetActive(false);
        }

        private void OnDestroyAgent(Agent agent)
        {
            Destroy(agent.gameObject);
        }

        private void AgentService_OnAgentSpawnedHandler()
        {
            Agent agent = agentPool.Get();
            agent.Initialize(GetRandomPoint());
            var guid = Guid.NewGuid();
            agent.SetGuid(guid);
            activeAgents.Add(guid, agent);
            agent.OnDestinationReached += AgentService_OnAgentReachDestination;
            agentService.Value.RegisterAgent(guid);
        }

        private void AgentService_OnAgentRemovedHandler()
        {
            if (activeAgents.Count <= 0)
            {
                return;
            }

            var guid = activeAgents.Keys.First();
            var agent = activeAgents[guid];

            agent.OnDestinationReached -= AgentService_OnAgentReachDestination;
            agent.Dispose();
            activeAgents.Remove(guid);
            agentPool.Release(agent);
            agentService.Value.UnRegisterAgent(guid);
        }

        private void AgentService_OnAgentReachDestination(Guid agentId)
        {
            agentService.Value.AgentReachedDestination(agentId);
        }

        private void AgentService_OnAgentRemoveAllHandler()
        {
            var activeAgentCount = activeAgents.Count;

            for (int i = 0; i < activeAgentCount; i++)
            {
                AgentService_OnAgentRemovedHandler();
            }
        }

        private void TickService_OnTickChanged(float tickRate)
        {
            foreach (var agent in activeAgents.Values)
            {
                agent.ChangeSpeed(tickRate);
            }
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }
    }
}