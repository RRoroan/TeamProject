using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombardmentSkillExplosion : MonoBehaviour
{
    private float explosionRadius;
    private LayerMask enemyLayer;
    private float explosionDelay = 0.8f; //폴발 애니메이션 시간

    //기본 데미지
    [SerializeField] private float addDamage = 10f;
    // 총합 데미지
    private float damage;
    private RangeWeaponHandler rangeWeaponHandler;

    private void Start()
    {
        rangeWeaponHandler = FindObjectOfType<RangeWeaponHandler>();
        damage = (addDamage + rangeWeaponHandler.Damage) * 2;
    }

    public void Init(float radius, LayerMask enemy)
    {
        explosionRadius = radius;
        enemyLayer = enemy;
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            ResourceController resource = enemy.GetComponent<ResourceController>();
            if (resource != null)
            {
                resource.ChangeHealth(-damage);
                Debug.Log($" {enemy.name}에게 {damage} 데미지 ");
            }
        }

        
        Destroy(gameObject); 

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
