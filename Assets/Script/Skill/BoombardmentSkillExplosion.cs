using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombardmentSkillExplosion : MonoBehaviour
{
    private float explosionRadius;
    private LayerMask enemyLayer;
    private float explosionDelay = 0.8f; //���� �ִϸ��̼� �ð�

    //�⺻ ������
    [SerializeField] private float addDamage = 10f;
    // ���� ������
    private float damage;

    private void Start()
    {
        damage = (addDamage /*+ �÷��̾� ������*/) * 2;
    }

    public void Init(float radius, LayerMask enemy)
    {
        explosionRadius = radius;
        enemyLayer = enemy;
    }

    //private IEnumerator ExplodeAfterDelay()
    //{
    //    yield return new WaitForSeconds(explosionDelay);

    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        ResourceController resource = new ResourceController();
    //        if (resource != null)
    //        {
    //            resource.ChangeHealth(-damage);
    //        }
    //    }

        
    //    Destroy(gameObject); 

    //}

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            ResourceController resource = enemy.GetComponent<ResourceController>();
            if (resource != null)
            {
                resource.ChangeHealth(-damage);
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
