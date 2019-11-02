public interface IProjectile
{
    void AddProjectileForce(float force);
    bool CheckLifeTime();
    void DestroyProjectile();
}
