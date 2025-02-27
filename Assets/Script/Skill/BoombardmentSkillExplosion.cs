using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoombardmentSkillExplosion : MonoBehaviour
{
    private float explosionRadius;
    private LayerMask enemyLayer;

    private Vector2 targetPosition;
    private bool isMoving = true;


    //기본 데미지
    [SerializeField] private float addDamage;
    // 총합 데미지
    private float damage;
    private RangeWeaponHandler rangeWeaponHandler;

    private Vector2 startPosition;
    private float speed;
    private float arrivalTime;
    private float elapsedTime = 0f;


    private void Update()
    {
        if (!isMoving) return;

        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / arrivalTime;
        transform.position = Vector2.Lerp(startPosition, targetPosition, progress);

        if (elapsedTime >= arrivalTime)
        {
            isMoving = false;
            TriggerExplosion();
        }

    }

    public void Init(int _damage, float radius, LayerMask enemy, float _arriveTime, Vector2 _targetPosition)
    {
        addDamage = _damage;
        explosionRadius = radius;
        enemyLayer = enemy;
        arrivalTime = _arriveTime;
        targetPosition = _targetPosition;

        startPosition = transform.position;

        // 이동거리를 계산 후 그에 따른 속도 설정
        float distance = Vector2.Distance(startPosition, targetPosition);
        speed = distance / arrivalTime;


        rangeWeaponHandler = FindObjectOfType<RangeWeaponHandler>();
        damage = (addDamage + rangeWeaponHandler.Damage) * 2;
    }

    public Quaternion RotateToTarget()
    {
        // 방향을 목표 위치로 회전시키기
        Vector2 direction = (targetPosition - startPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion playerRotate = Quaternion.Euler(0, 0, angle + 90);

        return playerRotate;
    }

    public void TriggerExplosion()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Explode");
        }
        
    }

    public void Attack()
    {
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
    }

    public void ExplosionDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
