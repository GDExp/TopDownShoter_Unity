using Character;

namespace GameCore
{
    class ChangeAnimationSpeedCommand : BaseAnimationCommand<AnimationSpeedValue<AbstractCharacter>>
    {
        public ChangeAnimationSpeedCommand(AbstractCharacter invoker, SpeedStatus speedStatus) : base(invoker)
        {
            argValue = new AnimationSpeedValue<AbstractCharacter>(invoker, speedStatus);

            receivers.Add(invoker.GetAnimationController() as IReceiver<AnimationSpeedValue<AbstractCharacter>>);
            receivers.Add(invoker.GetNavigationController() as IReceiver<AnimationSpeedValue<AbstractCharacter>>);
            receivers.Add(invoker.GetStatusController() as IReceiver<AnimationSpeedValue<AbstractCharacter>>);
        }
    }
}
