using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSizeDetecte : MonoBehaviour
{
    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    // Start is called before the first frame update
    void Start()
    {
        DetectMapBounds();
    }

    public void DetectMapBounds()
    {
        TilemapCollider2D tilemapCollider = GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            Debug.LogError("TilemapCollider2D�� �������� �ʽ��ϴ�!");
            return;
        }

        // Tilemap Collider�� ����(Bounds)�� ������
        Bounds bounds = tilemapCollider.bounds;
        // bounds.min : ���� ���� �ϴ� ��ǥ
        // bounds.max : ���� ���� ��� ��ǥ
        mapMinBounds = bounds.min;
        mapMaxBounds = bounds.max;
    }
    // ���� ���ϴ� ��ǥ�� ������
    public Vector2 GetMinBounds() => mapMinBounds;
    // ���� ���� ��ǥ�� ������
    public Vector2 GetMaxBounds() => mapMaxBounds;
    
}
