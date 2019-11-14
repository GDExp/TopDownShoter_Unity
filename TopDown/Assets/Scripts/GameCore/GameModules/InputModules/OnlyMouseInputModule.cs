using UnityEngine;
using Character;

namespace GameCore
{
    public class OnlyMouseInputModule : PlayerInputModule
    {
        private const float CastRadius = 1f;

        private Transform _target;

        private float _stopDistance;
        
        public OnlyMouseInputModule(PlayerCharacter owner) : base(owner)
        {
            isJoystick = false;
            _stopDistance = owner.NavigationController.GetAgentStopDistance();
        }

        protected override void LookAt()
        {
            if (_target is null) return;
            base.LookAt();
        }

        protected override void SetMoveValue()
        {
            base.SetMoveValue();
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    _target = null;
                    movePoint = hit.point;
                    var castResult = Physics.SphereCastAll(ray, CastRadius);
                    for (int i = 0; i < castResult.Length; ++i)
                    {
                        if (castResult[i].collider.CompareTag("Enemy"))
                        {
                            Debug.Log("!!!!");
                            _target = castResult[i].transform;
                            break;
                        }
                    }
                }
            }
            if (_target != null) movePoint = _target.position;
            isMove = CheckDistance();
        }

        private bool CheckDistance()
        {
            return (player.position - movePoint).magnitude >= ((_target is null) ? _stopDistance : owner.attackDistance);
        }

        protected override void SetAttackValue()
        {
            base.SetAttackValue();
            isAttack = !isMove && _target != null;
        }
    }
}
