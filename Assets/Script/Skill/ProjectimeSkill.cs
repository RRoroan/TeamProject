using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : BaseSkill
{
    // 투사채 프리팹
    [SerializeField] private GameObject projectilePrefab;

    [Header("투사체 설정")]
    // 투사체 속도
    [SerializeField] private float projectileSpeed = 5f;
    // 투사체 사이즈
    [SerializeField] private float projectileSize = 0.5f;
    // 투사체 갯수
    [SerializeField] private int projectileCount = 1;
    // 투사체가 쏘아질 간격(projectileCount 가 2 이상일때) 투사체 갯수가 2개 이상일 때
    [SerializeField] private float projectileInterval = 0.3f;
    // 관통 여부
    [SerializeField] private bool ispierce = true;

    [Header("타겟")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("탄두 설정")]
    [SerializeField] private float arrivalTime = 2f;

    // 모든 투사체 갯수
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
            Debug.LogError($"UseSkill: {SkillName} 오브젝트가 비활성화 상태여서 실행 불가능!");
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
                Debug.Log("타겟이 없습니다.");
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
        // 플레이어 주변 원형반경(플레이어 공격범위) 안에 있는 Collider2D를 찾아서 Collider2D 배열에 저장.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(playerController.transform.position, statHandler.GetRange(), enemyLayer);
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
