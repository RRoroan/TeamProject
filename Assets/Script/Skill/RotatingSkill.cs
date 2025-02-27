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

    // ����ä ����
    [SerializeField] private int projectileCount = 3;

    // ������ ���ظ� ���� ����
    [SerializeField] private float hitInterval = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

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
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} ������Ʈ�� ��Ȱ��ȭ ����!");
            return;
        }

        StartCoroutine(SpawnWeapon());

    }

    private IEnumerator SpawnWeapon()
    {


        ClearBeforeWeapon();

        // ������ �����ϰ� ����ä�� ��ġ
        float weaponInterval = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * weaponInterval;
            Vector2 spawnPosition = (Vector2)playerController.transform.position + GetPositionAngle(angle);


            GameObject weapon = Instantiate(rotatingPrefab, spawnPosition, Quaternion.identity);
            weapon.transform.parent = playerController.transform;

            RotatingSkillProjectile controller = weapon.GetComponent<RotatingSkillProjectile>();
            if (controller != null)
            {
                controller.Init(playerController.transform, range, angle, rotationSpeed, hitInterval, enemyLayer);
            }

            activeWeapon.Add(weapon);

        }

        if (lifetime < cooldown)
        {
            yield return new WaitForSeconds(lifetime);
            ClearBeforeWeapon();
        }



    }

    // �÷��̾� �ֺ� ���� �ݰ濡 ������
    private Vector2 GetPositionAngle(float angle)
    {
        float radius = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radius) * range, Mathf.Sin(radius) * range);
    }

    // ������ �ִ� ���� ����
    private void ClearBeforeWeapon()
    {
        foreach (GameObject weapon in activeWeapon)
        {
            Destroy (weapon);
        }
        activeWeapon.Clear();
    }

    public override void SkillLevelUp()
    {
        base.SkillLevelUp();
        
        if (skillLevel % 2 == 1)
        {
            if (lifetime <= cooldown)
            {
                lifetime += 0.5f;

            }
            else
            {
                damage++;
            }
        }
        if (skillLevel % 3 == 0)
        {
            projectileCount++;
        }
        if (skillLevel % 5 == 0)
        {
            rotationSpeed += 10;
            if (hitInterval <= 0.1f)
            {
                hitInterval = Mathf.Max(hitInterval - 0.05f, 0.1f);

            }
            damage += 3;
        }

        damage++;

    }

}
