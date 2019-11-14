using Character;

namespace GameCore
{
    class ChangeAnimationSpeedCommand : BaseAnimationCommand<AnimationSpeedValue<AbstractCharacter>>
    {
        public ChangeAnimationSpeedCommand(AbstractCharacter invoker, SpeedStatus speedStatus) : base(invoker)
        {
            argValue = new AnimationSpeedValue<AbstractCharacter>(invoker, speedStatus);
            
            receivers.Add(invoker.NavigationController as IReceiver<AnimationSpeedValue<AbstractCharacter>>);
            receivers.Add(invoker.StatusController as IReceiver<AnimationSpeedValue<AbstractCharacter>>);
        }
    }
}
