using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("RangeWeapon Info")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] float buleetSize = 1f;
    public float BulletSize
    {
        get => buleetSize;
        set { buleetSize = Mathf.Max(0.5f, value); }

    }

    [SerializeField] private float duration;
    public float Duration { get => duration; }

    [SerializeField] private float spread;
    public float Spread { get => spread; }

    [SerializeField] private int numberoProjectilesPerShot;
    public int NumberoProjectilesPerShot
    {
        get => numberoProjectilesPerShot;
        set { numberoProjectilesPerShot = Mathf.Max(1, value); }
    }


    [SerializeField] private float multipleProjectileAngle;
    public float MultipleProjectileAngel { get => multipleProjectileAngle; }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get => projectileColor; }


    private ProjectileManager projectileManager;
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("Attack2");
        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberoProjectilesPerShot;

        float minAngle = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace + 0.5f * multipleProjectileAngle;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet
            (this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degress)
    {
        return Quaternion.Euler(0, 0, degress) * v;
    }

}
