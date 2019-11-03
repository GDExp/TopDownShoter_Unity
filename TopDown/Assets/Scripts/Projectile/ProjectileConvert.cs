using System;
using System.Collections.Generic;

//test pool object
public class ProjectileConvert
{
    private Dictionary<ProjectileType, Type> _projectiles;

    public ProjectileConvert()
    {
        _projectiles = new Dictionary<ProjectileType, Type>
        {
            { ProjectileType.PlayerProjectile_Test, typeof(TestProjectile) },
            { ProjectileType.EnemyProjectile_Test, typeof(EnemyTestProjectile) },
        };
    }

    public Type GetConvertProjectileType(ProjectileType type)
    {
        Type currentType = typeof(TestProjectile);
        if (_projectiles.ContainsKey(type)) currentType = _projectiles[type];
        return currentType;
    }
}
