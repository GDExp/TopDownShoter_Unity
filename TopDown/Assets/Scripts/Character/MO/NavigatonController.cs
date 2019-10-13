using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public interface IWayMaker
    {
        void MakeWayPath(Vector3 point);
    }

    class NavigatonController : IWayMaker
    {
        private readonly NavMeshAgent _agent;
        public bool isMoved;


        public NavigatonController( AbstractCharacter character)
        {
            _agent = character.GetComponent<NavMeshAgent>();
        }

        public void MakeWayPath(Vector3 point)
        {
            _agent?.SetDestination(point);
        }
    }
}
