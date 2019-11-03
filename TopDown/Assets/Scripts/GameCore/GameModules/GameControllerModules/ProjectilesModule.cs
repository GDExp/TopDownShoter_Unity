namespace GameCore
{
    class ProjectilesModule : BaseGameControllerModule<IProjectile>
    {
        public ProjectilesModule(GameController gameController) : base(gameController)
        {
            elementEvent += UpdateProjectile;
        }

        private void UpdateProjectile(IProjectile projectile)
        {
            if (!projectile.CheckLifeTime()) return;
            projectile.DestroyProjectile();
            RemoveElementInList(projectile);
        }
    }
}
