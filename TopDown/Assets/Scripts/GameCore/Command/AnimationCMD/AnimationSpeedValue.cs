using Character;

namespace GameCore
{
    public readonly struct AnimationSpeedValue<T>
        where T: AbstractCharacter
    {
        public readonly SpeedStatus speedStatus;
        public readonly float characterSpeed;
        public readonly float multiplier;

        public AnimationSpeedValue(T owner, SpeedStatus speedStatus)
        {
            this.speedStatus = speedStatus;

            var status = owner?.StatusController;

            multiplier = status.CalculationSpeedModificator(speedStatus);
            characterSpeed = status.CalculationCharacterSpeed(speedStatus);
        }
    }
}

