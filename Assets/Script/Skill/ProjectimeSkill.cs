using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : BaseSkill
{
    // ����ä ������
    [SerializeField] private GameObject projectilePrefab;
    // ����ä �߻� ��ġ
    [SerializeField] private Transform firePoint;
    // ����ü �ӵ�
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private LayerMask enemyLayer;

    public int projectileCount = 1;


    public override void UseSkill()
    {
        for (int i = 0; i < statHandler.GetProjectileCount(); i++)
        {
            Collider2D target = FindClosetEnemy();
        }
    }

    private Collider2D FindClosetEnemy()
    {
        // �÷��̾� �ֺ� �����ݰ�(�÷��̾� ���ݹ���) �ȿ� �ִ� Collider2D�� ã�Ƽ� Collider2D �迭�� ����.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, statHandler.GetRange(), enemyLayer);
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
