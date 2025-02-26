using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeaponController : MonoBehaviour
{
    private Transform player;
    private float range;
    private float rotationSpeed;
    private float lifetime;
    private float hitInterval;
    private float angle;
    private Dictionary<Collider2D, float> lastHitTime = new Dictionary<Collider2D, float>();

    public void Init(Transform _player, float _range, float _angle, float _rotationSpeed, float _lifetime, float _hitInterval)
    {
        player = _player;
        range = _range;
        angle = _angle;
        rotationSpeed = _rotationSpeed;
        lifetime = _lifetime;
        hitInterval = _hitInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null ) return;

        angle += rotationSpeed * Time.deltaTime;
        Vector2 newPosition = (Vector2)player.position + new Vector2(Mathf.Cos(angle *
            Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * range;
        transform.position = newPosition;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if ()
        //{
            
        //}
    }

}
