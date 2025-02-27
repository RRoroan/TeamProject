using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSkillBullet : MonoBehaviour
{
    private MapSizeDetecte mapSize;
    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    // ÃÑÇÕ ´ë¹ÌÁö(ÇÃ·¹ÀÌ¾î °ø°Ý·Â + addDamage)
    private float damage;
    private LayerMask enemyLayer;
    private bool isPierce;

    private void Start()
    {
        mapSize = GameManager.Instance.mapSize;
        // ¸ÊÀÇ ÁÂÇÏ´Ü ÁÂÇ¥
        mapMinBounds = mapSize.GetMinBounds();
        // ¸ÊÀÇ ¿ì»ó´Ü ÁÂÇ¥
        mapMaxBounds = mapSize.GetMaxBounds();

    }

    void Update()
    {
        if ((transform.position.x < mapMinBounds.x) || (transform.position.x > mapMaxBounds.x)
            || (transform.position.y < mapMinBounds.y) || (transform.position.y > mapMaxBounds.y))
        {
            Destroy(gameObject, 1f);
        }

    }

    public void Init(bool _isPierce, LayerMask enemy, int _damage)
    {
        enemyLayer = enemy;
        isPierce = _isPierce;
        damage = _damage;

        
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
