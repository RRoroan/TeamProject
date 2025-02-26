using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkill : BaseSkill
{
    // ����� ������
    [SerializeField] private GameObject rotatingPrefab;

    // ��ų�� ���ƴٴ� ����
    [SerializeField] private float range = 2f;

    // ��ų�� ���ƴٴ� �ӵ�
    [SerializeField] private float rotationSpeed = 100f;

    // ��ų�� ���ӵ� �ð�
    [SerializeField] private float lifetime = 3f;

    // ������ ���ظ� ���� ����
    [SerializeField] private float hitInterval = 0.5f;

    [SerializeField] private int projectileCount = 1;

    // Ȱ��ȭ �� ���⸦ �־��� ����Ʈ(projectileCount ��ŭ �־��ٲ���)
    private List<GameObject> activeWeapon = new List<GameObject>();

    // �ߺ� ������ ����
    private HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();


    protected override void Start()
    {
        base.Start();
        projectileCount += statHandler.GetProjectileCount();
    }

    public override void UseSkill()
    {
        if (isCooldown) return;
        StartCoroutine(SkillCooldown());

    }

    private void SpawnWeapon()
    {
        foreach (GameObject weapon in activeWeapon)
        {
            Destroy(weapon);
        }
        activeWeapon.Clear();

        // ������ �����ϰ� ����ä�� ��ġ
        float weaponInterval = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * weaponInterval;
            Vector2 spawnPosition = (Vector2)player.transform.position + GetPositionAngle(angle);
            GameObject weapon = Instantiate(rotatingPrefab, spawnPosition, Quaternion.identity);
            weapon.transform.parent = player.transform;

            

        }


    }

    private Vector2 GetPositionAngle(float angle)
    {
        float radius = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radius) * radius, Mathf.Sin(radius) * radius);
    }

}
