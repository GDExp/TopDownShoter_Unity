using UnityEngine;
using Character;

namespace GameCore
{
    public class PlayerInputModule : BaseCharacterUpdatableModule<PlayerCharacter>
    {
        protected readonly Camera camera;
        protected readonly Transform cameraTransform;
        protected readonly Transform player;
        protected readonly StatusController status;

        public Vector3 movePoint { get; protected set; }

        public bool isMove { get; protected set; }
        public bool isAttack { get; protected set; }
        public bool isJoystick { get; protected set; }

        public PlayerInputModule(PlayerCharacter owner) : base(owner)
        {
            camera = Camera.main;
            cameraTransform = camera.transform;
            player = owner.transform;
            status = owner.GetStatusController() as StatusController;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            this.LookAt();
            this.SetMoveValue();
            this.SetAttackValue();
        }

        protected virtual void LookAt()
        {
            Vector3 direction = Vector3.zero;

            if (isJoystick) direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.y - player.position.y));
            else direction = movePoint;

            Quaternion look_direction = Quaternion.LookRotation(direction - player.position);
            player.localRotation = Quaternion.Euler(player.up * look_direction.eulerAngles.y);
            player.TransformDirection(direction - player.position);
        }
        protected virtual void SetMoveValue() { }
        protected virtual void SetAttackValue() { }
    }
}
