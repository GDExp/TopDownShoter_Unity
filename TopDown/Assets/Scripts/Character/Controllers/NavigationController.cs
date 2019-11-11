using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameCore;

namespace Character
{
    public interface IWayMaker
    {
        void MakeWayPath(Vector3 point);
    }

    public class NavigationController : IWayMaker , IReceiver<AnimationSpeedValue<AbstractCharacter>>
    {
        private readonly NavMeshAgent _agent;
        private readonly NavMeshObstacle _obstacle;
        private readonly Dictionary<SpeedStatus, float> _characterSpeedByStatus;
        private Vector3 _currentPoint;


        public NavigationController( AbstractCharacter character)
        {
            var characterComponent = character.GetComponent<CharacterController>();

            _agent = character.GetComponent<NavMeshAgent>();
            _agent.radius = characterComponent.radius;
            _agent.height = characterComponent.height;

            _obstacle = character.GetComponent<NavMeshObstacle>();
            _obstacle.carving = true;
            _obstacle.carveOnlyStationary = false;
            _obstacle.shape = NavMeshObstacleShape.Capsule;
            _obstacle.radius = characterComponent.radius;
            _obstacle.height = characterComponent.height;
            _obstacle.enabled = false;

            _characterSpeedByStatus = character.characterValue.GetChracterSpeedByStatus();
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

        //IReceicer
        public void HandleCommand(AnimationSpeedValue<AbstractCharacter> inputValue)
        {
            _agent.speed = _characterSpeedByStatus[inputValue.speedStatus];
        }
    }
}
