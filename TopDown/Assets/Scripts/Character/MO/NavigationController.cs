using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public interface IWayMaker
    {
        void MakeWayPath(Vector3 point);
    }

    public class NavigationController : IWayMaker
    {
        private readonly NavMeshAgent _agent;
        private readonly NavMeshObstacle _obstacle;
        private Vector3 _currentPoint;


        public NavigationController( AbstractCharacter character)
        {
            _agent = character.GetComponent<NavMeshAgent>();
            _obstacle = character.GetComponent<NavMeshObstacle>();
            _obstacle.enabled = false;
        }

        public void MakeWayPath()
        {
            _agent?.SetDestination(_currentPoint);
        }

        public void MakeWayPath(Vector3 point)
        {
            _agent?.SetDestination(point);
        }

        public bool InteractObstacleComponent()
        {
            _agent.enabled = !_agent.isActiveAndEnabled;
            _obstacle.enabled = !_obstacle.isActiveAndEnabled;
            return _agent.isActiveAndEnabled;
        }

        public void SetCurrentPoint(Vector3 point)
        {
            _currentPoint = point;
        }

        public float GetAgentStopDistance()
        {
            return _agent.stoppingDistance + 0.5f;
        }
    }
}
