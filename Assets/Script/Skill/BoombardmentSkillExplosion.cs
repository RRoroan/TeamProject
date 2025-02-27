using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombardmentSkillExplosion : MonoBehaviour
{
    private float explosionRadius;
    private LayerMask enemyLayer;

    //�⺻ ������
    [SerializeField] private float addDamage = 10f;
    // ���� ������
    private float damage;

    // Attack()�� ������ �������� �ߺ��� ������ ������ ���ع��� �ʰ� �����ϱ� ���� ����
    private HashSet<Collider2D> damagedEnemies = new HashSet<Collider2D>();

    private void Start()
    {
        damage = (addDamage /*+ �÷��̾� ������*/) * 2;
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
