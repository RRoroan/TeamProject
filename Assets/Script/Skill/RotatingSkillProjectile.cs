using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RotatingSkillProjectile : MonoBehaviour
{
    private Transform player;
    private float range;
    private float hitInterval;
    private float angle;
    private float rotationSpeed;
    private LayerMask enemyLayer;

    // 스킬 자체 데미지
    [SerializeField] private float addDamage = 5f;
    // 총함 데미지(addDamage + 플레이어 공격력)
    private float damage;



    private float attackTimer = 0f;

    // 키 : Collider2D (공격한 적) float(마지막으로 공격한 시간) 
    private Dictionary<Collider2D, float> lastHitTime = new Dictionary<Collider2D, float>();

    public void Init(Transform _player, float _range, float _angle, float _rotationSpeed
        , float _hitInterval, LayerMask _enemyLayer)
    {
        player = _player;
        range = _range;
        angle = _angle;
        rotationSpeed = _rotationSpeed;
        hitInterval = _hitInterval;
        enemyLayer = _enemyLayer; 

    }

    private void Start()
    {
        damage = addDamage; // + 플레이어 공격력
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null ) return;

        //회전 속도
        angle += Time.deltaTime * rotationSpeed;

        Vector2 newPos = player.position + (Vector3)GetPositionAroundPlayer();
        transform.position = newPos;

        transform.rotation = Quaternion.identity;

        attackTimer += Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (attackTimer >= hitInterval)
            {
                attackTimer = 0f;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (CanAttack(enemy))
                    {
                        ResourceController resourceController = enemy.GetComponent<ResourceController>();
                        if (resourceController != null)
                        {
                            resourceController.ChangeHealth(-damage);
                            lastHitTime[enemy] = Time.time;
                        }
                    }
                }
            }
        }
    }



    private Vector2 GetPositionAroundPlayer()
    {
        float radius = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(radius) * range,
            Mathf.Sin(radius) * range);
    }

    //private void Attack()
    //{
    //    // 현재 투사채 위치(transform.position), (탐색 반경), (적의 레이어)
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        if (CanAttack(enemy))
    //        {
    //            ResourceController resourceController = enemy.GetComponent<ResourceController>();
    //            if (resourceController != null)
    //            {
    //                resourceController.ChangeHealth(-damage);
    //                lastHitTime[enemy] = Time.time;
    //            }
    //        }
    //    }
    //}

    // 중복방지(hitInterval로 타격간격 조정)
    private bool CanAttack(Collider2D enemy)
    {
        if (lastHitTime.ContainsKey(enemy))
        {
            if (Time.time - lastHitTime[enemy] < hitInterval)
            {
                return false;
            }
        }
        return true;
    }

}
