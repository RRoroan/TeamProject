using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public StatHandler statHandler;

    [SerializeField] private float attackRange = 10f;

    [Header("Attack Info")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 1f;
    public float Speed { get => speed; set => speed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float Damage { get => damage; set => damage = value; }


    public LayerMask target;

    private SpriteRenderer weaponRenderer;

    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private int bulletIndex;
    public int BulletIndex {  get { return bulletIndex; } }

    [Header("Knockback Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    public AudioClip attackSoundClip;
    public BaseController Controller { get; private set; }


    private void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        weaponRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        //statHandler = GameManager.Instance.GetStatHandler(); 
    }

    protected virtual void Update()
    {

    }

    public virtual void Attack()
    {
        if(attackSoundClip)
            SoundManager.PlayClip(attackSoundClip);
    }

    protected virtual void Rotate(bool isLeft)
    {

    }


}
