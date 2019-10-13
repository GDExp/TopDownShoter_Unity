
namespace Character
{
    class PlayerCharacter: AbstractCharacter
    {

        protected override void SetupCharacter()
        {
            base.SetupCharacter();
            GameCore.GameController.Instance.SetPlayerReference(gameObject);
        }
    }
}
