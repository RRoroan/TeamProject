using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSkillBullet : MonoBehaviour
{
    private MapSizeDetecte mapSize;
    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    // 투사체 피해량 
    [SerializeField] private float addDamage = 5f;
    // 총합 대미지(플레이어 공격력 + addDamage)
    private float damage;
    private LayerMask enemyLayer;
    private bool isPierce;

    private void Start()
    {
        mapSize = StageManager.Instance.mapSize;
        // 맵의 좌하단 좌표
        mapMinBounds = mapSize.GetMinBounds();
        // 맵의 우상단 좌표
        mapMaxBounds = mapSize.GetMaxBounds();

        damage = addDamage; // + player damage 추가
    }

    void Update()
    {
        if ((transform.position.x < mapMinBounds.x) || (transform.position.x > mapMaxBounds.x)
            || (transform.position.y < mapMinBounds.y) || (transform.position.y > mapMaxBounds.y))
        {
            Destroy(gameObject, 1f);
        }

    }

    public void Init(bool _isPierce, LayerMask enemy)
    {
        enemyLayer = enemy;
        isPierce = _isPierce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1<< collision.gameObject.layer)) != 0 )
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-damage);
            }

            if (!isPierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
