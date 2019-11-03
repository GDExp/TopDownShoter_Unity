using UnityEngine;

public enum ProjectileType
{
    PlayerProjectile_Test,
    EnemyProjectile_Test,
}

[System.Serializable]
public class Projectile : PropertyAttribute
{
    public ProjectileType projectileType;
    public BaseProjectile projectilePrefab;

    public Projectile(ProjectileType type, BaseProjectile prefab)
    {
        projectileType = type;
        projectilePrefab = prefab;
    }
}

[CreateAssetMenu(fileName = "ProjectileLibrary", menuName = "CustomSO/Projectile/New Library", order = 100)]
public class ProjectileLibrarySO : ScriptableObject
{
    public Projectile[] projectiles;
}
