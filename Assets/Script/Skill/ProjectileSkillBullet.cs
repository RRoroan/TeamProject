using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSkillBullet : MonoBehaviour
{
    private MapSizeDetecte mapSize;
    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    private void Start()
    {
        mapSize = GameManager.Instance.mapSize;
        // ���� ���ϴ� ��ǥ
        mapMinBounds = mapSize.GetMinBounds();
        // ���� ���� ��ǥ
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
}
