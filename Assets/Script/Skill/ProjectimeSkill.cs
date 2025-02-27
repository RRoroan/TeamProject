using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : BaseSkill
{
    // ����ä ������
    [SerializeField] private GameObject projectilePrefab;

    [Header("����ü ����")]
    // ����ü �ӵ�
    [SerializeField] private float projectileSpeed = 5f;
    // ����ü ������
    [SerializeField] private float projectileSize = 0.5f;
    // ����ü ����
    [SerializeField] private int projectileCount = 1;
    // ����ü�� ����� ����(projectileCount �� 2 �̻��϶�) ����ü ������ 2�� �̻��� ��
    [SerializeField] private float projectileInterval = 0.3f;
    // ���� ����
    [SerializeField] private bool ispierce = true;

    [Header("Ÿ��")]
    [SerializeField] private LayerMask enemyLayer;

    Coroutine skillCoroutine;

    // ��� ����ü ����
    private int allProCount;

    protected override void Start()
    {
        base.Start();
        allProCount = GetCurrentProjectileCount();
    }


    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} ������Ʈ�� ��Ȱ��ȭ ���¿��� ���� �Ұ���!");
            return;
        }
        //if (skillCoroutine != null)
        //{
        //    StopCoroutine(skillCoroutine);
        //    skillCoroutine = null;
        //}

        skillCoroutine = StartCoroutine(FireProjectiles());
      
    }

    private IEnumerator FireProjectiles()
    {
        int totalProjectileCount = GetCurrentProjectileCount();
        for (int i = 0; i < totalProjectileCount; i++)
        {
            float currentProjectileSize = projectileSize;
            int currentDamage = damage;

            Collider2D target = FindClosetEnemy();

            if (target != null)
            {
                FireProjectile(target, currentDamage, currentProjectileSize);
            }
            else
            {
                Debug.Log("Ÿ���� �����ϴ�.");
            }
        yield return new WaitForSeconds(projectileInterval);
        }

    }

    private void FireProjectile(Collider2D target, int _damage, float _skillSize)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(_skillSize, _skillSize, 1f);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
        {
            Vector2 direction = (target.transform.position - firePoint.position).normalized;
            rigidbody.velocity = direction * projectileSpeed;
        }

        ProjectileSkillBullet bullet = projectile.GetComponent<ProjectileSkillBullet>();
        if (bullet != null)
        {
            bullet.Init(ispierce, enemyLayer, _damage);
        }

    }

    private Collider2D FindClosetEnemy()
    {
        // �÷��̾� �ֺ� �����ݰ�(�÷��̾� ���ݹ���) �ȿ� �ִ� Collider2D�� ã�Ƽ� Collider2D �迭�� ����.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(playerController.transform.position, statHandler.GetRange(), enemyLayer);
        // �ּ� �Ÿ��� ����
        float minDistance = Mathf.Infinity;
        // ���� ����� ���� ������ ����
        Collider2D closetEnemy = null;

        foreach (Collider2D enemy in enemies)
        {
            // �÷��̾�� �� ������ �Ÿ��� ����Ѵ�.
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            // �� ����� ���� ã���� closetEnemy�� miDistance�� �ٽ� �����Ѵ�.
            if (distance < minDistance)
            {
                minDistance = distance;
                closetEnemy = enemy;
            }
        }
        return closetEnemy;

    }

    private int GetCurrentProjectileCount()
    {
        allProCount = projectileCount + statHandler.GetProjectileCount();
        return allProCount;
    }

    public override void SkillLevelUp()
    {
        base.SkillLevelUp();
        if (skillLevel % 2 == 1)
        {
            if (projectileSize <= 2)
            {
                projectileSize = Mathf.Min(projectileSize + 0.02f, 2);
                Debug.Log("����ü ������ ����");
            }
            else
            {
                damage++;
            }
        }
        else
        {
            if (cooldown >= 5)
            {
                cooldown = Mathf.Max(cooldown - 0.05f, 5);
                Debug.Log("����ü ��Ÿ�� ����");
            }
            else
            {
                damage++;
            }
        }

        if (skillLevel % 3 == 0)
        {
            if (projectileCount <= 10)
            {
                projectileCount = Mathf.Min(projectileCount + 1, 10);
                Debug.Log("����ü ���� ����");
            }
            else
                damage++;
        }
        damage++;

        SkillManagerReset();

        //if (skillCoroutine != null)
        //{
        //    StopCoroutine(skillCoroutine);
        //    skillCoroutine = null;
        //}

    }

    public IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds((int)(cooldown / 2));
        StopCoroutine(skillCoroutine);
        skillCoroutine = null;
        UseSkill();
    }


}
