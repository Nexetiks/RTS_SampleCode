using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agent
{
    public class Agent : MonoBehaviour
    {
        public event Action<Guid> OnDestinationReached;
        private readonly IPathfinding pathfindingService = new DefaultPathfinding();
        private bool isMoving;
        private Tween currentTween;
        private Guid agentId;

        public void Initialize(Vector3 startPoint)
        {
            transform.position = startPoint;
            PickNewTarget();
        }

        public void SetGuid(Guid guid)
        {
            agentId = guid;
        }

        public void Dispose()
        {
            currentTween.Kill();
            isMoving = false;
        }

        public void ChangeSpeed(float speed)
        {
            currentTween.timeScale = speed;
        }

        private void OnTickChanged(float tickRate)
        {
            currentTween.timeScale = tickRate;
        }

        private void PickNewTarget()
        {
            if (isMoving)
            {
                return;
            }

            Vector3 target = GetRandomPoint();
            MoveToTarget(target);
        }

        private void MoveToTarget(Vector3 target)
        {
            isMoving = true;

            List<Vector3> path = pathfindingService.GetPath(transform.position, target);

            if (path.Count > 0)
            {
                MoveAlongPath(path);
            }
            else
            {
                MoveDirectlyToTarget(target);
            }
        }

        private void MoveDirectlyToTarget(Vector3 target)
        {
            currentTween = transform.DOMove(target, 2f).OnComplete(() =>
            {
                ReachedDestination();
                isMoving = false;
                PickNewTarget();
            });
        }

        private void MoveAlongPath(List<Vector3> path)
        {
            if (path.Count == 0)
            {
                ReachedDestination();
                isMoving = false;
                PickNewTarget();

                return;
            }

            Vector3 nextPoint = path[0];
            path.RemoveAt(0);

            currentTween = transform.DOMove(nextPoint, 2f).OnComplete(() =>
            {
                MoveAlongPath(path);
            });
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }

        private void ReachedDestination()
        {
            OnDestinationReached?.Invoke(agentId);
        }
    }
}