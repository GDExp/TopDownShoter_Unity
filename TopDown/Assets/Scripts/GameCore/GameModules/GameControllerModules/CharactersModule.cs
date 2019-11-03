using Character;

namespace GameCore
{
    class CharactersModule : BaseGameControllerModule<ICharacter>
    {
        public AbstractCharacter player { get; private set; }

        public CharactersModule(GameController gameController) : base(gameController)
        {
            elementEvent += UpdateChracter;
        }

        public override void OnStart()
        {
            SetupCharactersInScene();
        }

        private void SetupCharactersInScene()
        {
            if (updatebleElements.Count == 0) return;
            foreach(var el in updatebleElements)
            {
                if(el is PlayerCharacter)
                {
                    player = el as PlayerCharacter;
                    break;
                } 
            }

            for (int i = 0; i < updatebleElements.Count; ++i) updatebleElements[i].InvokeSetupCharacter();
        }

        private void UpdateChracter(ICharacter character)
        {
            character.UpdateCharacter();
        }
    }
}
