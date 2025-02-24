using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public StatHandler statHandler;

    public LayerMask target;

    private SpriteRenderer weaponRenderer;

    [SerializeField] private int bulletIndex;
    public int BulletIndex {  get { return bulletIndex; } }

    [Header("Knockback Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    private void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
        statHandler = GameManager.Instance.GetStatHandler();
    }

    protected virtual void Update()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void Rotate(bool isLeft)
    {

    }


}
