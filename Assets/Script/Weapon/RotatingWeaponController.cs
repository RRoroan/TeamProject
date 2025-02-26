using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeaponController : MonoBehaviour
{
    private Transform player;
    private float range;
    private float hitInterval;
    private float angle;
    private float rotationSpeed;
    private LayerMask enemyLayer;


    private float attackTimer = 0f;


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
        if (attackTimer >= hitInterval)
        {
            Attack();
            attackTimer = 0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private Vector2 GetPositionAroundPlayer()
    {
        float radius = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(radius) * range,
            Mathf.Sin(radius) * range);
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
        foreach (Collider2D enmmy in hitEnemies)
        {
            // 적들의 정보를 가져오고 적들의 체력을 깍을 수 있게 해야합니다.
        }
    }

}
