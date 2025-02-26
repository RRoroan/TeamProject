using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : BaseSkill
{
    // ����ä ������
    [SerializeField] private GameObject projectilePrefab;
    // ����ü �ӵ�
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private LayerMask enemyLayer;

    protected override void Start()
    {
        base.Start();
    }


    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} ������Ʈ�� ��Ȱ��ȭ ���¿��� ���� �Ұ���!");
            return;
        }

        for (int i = 0; i < statHandler.GetProjectileCount(); i++)
        {
            Collider2D target = FindClosetEnemy();

            if (target != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = (target.transform.position - firePoint.position).normalized;
                    rb.velocity = direction * projectileSpeed;
                }
            }
            else
            {
                Debug.Log("Ÿ���� �����ϴ�.");
            }
        }
    }

    private Collider2D FindClosetEnemy()
    {
        // �÷��̾� �ֺ� �����ݰ�(�÷��̾� ���ݹ���) �ȿ� �ִ� Collider2D�� ã�Ƽ� Collider2D �迭�� ����.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, statHandler.GetRange(), enemyLayer);
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
