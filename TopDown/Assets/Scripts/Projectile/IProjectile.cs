using UnityEngine;

public interface IProjectile
{
    void SetupProjectile(Character.AbstractCharacter owner);
    void SetStartPosition(Transform point);
    void AddProjectileForce(float force);
    bool CheckLifeTime();
    void TakeDamage(Character.AbstractCharacter invoker);
    void DestroyProjectile();
}
