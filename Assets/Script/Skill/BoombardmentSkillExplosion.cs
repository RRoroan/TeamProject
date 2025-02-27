using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombardmentSkillExplosion : MonoBehaviour
{
    private float explosionRadius;
    private LayerMask enemyLayer;

    //기본 데미지
    [SerializeField] private float addDamage = 10f;
    // 총합 데미지
    private float damage;

    // Attack()를 여러번 넣을꺼라 중복된 적들이 여러번 피해받지 않게 설정하기 위한 변수
    private HashSet<Collider2D> damagedEnemies = new HashSet<Collider2D>();

    private void Start()
    {
        damage = (addDamage /*+ 플레이어 데미지*/) * 2;
    }

    public void Init(float radius, LayerMask enemy)
    {
        explosionRadius = radius;
        enemyLayer = enemy;
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            ResourceController resource = enemy.GetComponent<ResourceController>();
            if (!damagedEnemies.Contains(enemy))
            {

                if (resource != null)
                {
                    resource.ChangeHealth(-damage);
                    damagedEnemies.Add(enemy);
                }
            }
        }
    }

    private void ExplosionDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
