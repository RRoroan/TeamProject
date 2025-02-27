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

    [Header("ź�� ����")]
    [SerializeField] private float arrivalTime = 2f;

    // ��� ����ü ����
    private int AllproCount;

    protected override void Start()
    {
        base.Start();
        AllproCount = projectileCount + statHandler.GetProjectileCount();
    }


    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} ������Ʈ�� ��Ȱ��ȭ ���¿��� ���� �Ұ���!");
            return;
        }

        StartCoroutine(FireProjectiles());
      
    }

    private IEnumerator FireProjectiles()
    {
        AllproCount = projectileCount + statHandler.GetProjectileCount();
        for (int i = 0; i < AllproCount; i++)
        {
            Collider2D target = FindClosetEnemy();

            if (target != null)
            {
                FireProjectile(target);
            }
            else
            {
                Debug.Log("Ÿ���� �����ϴ�.");
            }
        yield return new WaitForSeconds(projectileInterval);
        }

    }

    private void FireProjectile(Collider2D target)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
        {
            Vector2 direction = (target.transform.position - firePoint.position).normalized;
            rigidbody.velocity = direction * projectileSpeed;
        }

        ProjectileSkillBullet bullet = projectile.GetComponent<ProjectileSkillBullet>();
        if (bullet != null)
        {
            bullet.Init(ispierce, enemyLayer);
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


}
