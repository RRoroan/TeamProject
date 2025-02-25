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
            Debug.LogError("TilemapCollider2D°¡ Á¸ÀçÇÏÁö ¾Ê½À´Ï´Ù!");
            return;
        }

        // Tilemap ColliderÀÇ ¹üÀ§(Bounds)¸¦ °¡Á®¿È
        Bounds bounds = tilemapCollider.bounds;
        // bounds.min : ¸ÊÀÇ ÁÂÃø ÇÏ´Ü ÁÂÇ¥
        // bounds.max : ¸ÊÀÇ ¿ìÃø »ó´Ü ÁÂÇ¥
        mapMinBounds = bounds.min;
        mapMaxBounds = bounds.max;
    }
    // ¸ÊÀÇ ÁÂÇÏ´Ü ÁÂÇ¥¸¦ °¡Á®¿È
    public Vector2 GetMinBounds() => mapMinBounds;
    // ¸ÊÀÇ ¿ì»ó´Ü ÁÂÇ¥¸¦ °¡Á®¿È
    public Vector2 GetMaxBounds() => mapMaxBounds;
    
}
