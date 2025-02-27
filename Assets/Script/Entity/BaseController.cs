using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] public Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;


    [SerializeField] protected WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;
    protected bool readytoAttack;
    private float timeSinceLastAttack = float.MaxValue;
    protected WeaponChanger weaponChanger;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        weaponChanger = GetComponent<WeaponChanger>();

        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();

        if (movementDirection != Vector2.zero)
        {
            Rotate(movementDirection);
        }

        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if (knockbackDuration > 0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        if (knockbackDuration > 0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    protected void Rotate(Vector2 direction)
    {
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
       
        float t = 10f;

        transform.rotation = Quaternion.Lerp(
            transform.rotation, Quaternion.Euler(0,0,rotz), Time.deltaTime * t);

        //if (weaponPivot != null)
        //{
        //    weaponPivot.rotation = Quaternion.Lerp(
        //        weaponPivot.rotation, Quaternion.Euler(0, 0, weaponRotZ), Time.deltaTime * t);
        //}
    }



    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    protected void HandleAttackDelay()
    {
        if (weaponHandler == null) return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (timeSinceLastAttack > weaponHandler.Delay && movementDirection == Vector2.zero)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }
    

    protected virtual void Attack()
    {
        if (movementDirection == Vector2.zero && readytoAttack)
        {
            weaponHandler?.Attack();
        }
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer rendere in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = rendere.color;
            color.a = 0.3f;
            rendere.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject);

    }
}
