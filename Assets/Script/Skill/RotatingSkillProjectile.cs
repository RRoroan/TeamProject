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

    // ��ų ��ü ������
    [SerializeField] private float addDamage = 5f;
    // ���� ������(addDamage + �÷��̾� ���ݷ�)
    private float damage;



    private float attackTimer = 0f;

    // Ű : Collider2D (������ ��) float(���������� ������ �ð�) 
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
        damage = addDamage; // + �÷��̾� ���ݷ�
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null ) return;

        //ȸ�� �ӵ�
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
    //    // ���� ����ä ��ġ(transform.position), (Ž�� �ݰ�), (���� ���̾�)
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

    // �ߺ�����(hitInterval�� Ÿ�ݰ��� ����)
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
