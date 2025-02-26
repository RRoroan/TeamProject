using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : BaseSkill
{
    // 투사채 프리팹
    [SerializeField] private GameObject projectilePrefab;
    // 투사체 속도
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
            Debug.LogError($"UseSkill: {SkillName} 오브젝트가 비활성화 상태여서 실행 불가능!");
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
                Debug.Log("타겟이 없습니다.");
            }
        }
    }

    private Collider2D FindClosetEnemy()
    {
        // 플레이어 주변 원형반경(플레이어 공격범위) 안에 있는 Collider2D를 찾아서 Collider2D 배열에 저장.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, statHandler.GetRange(), enemyLayer);
        // 최소 거리를 설정
        float minDistance = Mathf.Infinity;
        // 가장 가까운 적을 저장할 변수
        Collider2D closetEnemy = null;

        foreach (Collider2D enemy in enemies)
        {
            // 플레이어와 적 사이의 거리를 계산한다.
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            // 더 가까운 적을 찾으면 closetEnemy와 miDistance를 다시 설정한다.
            if (distance < minDistance)
            {
                minDistance = distance;
                closetEnemy = enemy;
            }
        }
        return closetEnemy;

    }


}
