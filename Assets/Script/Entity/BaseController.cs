using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;


    [SerializeField] public WeaponHandler WeaponPrefab;
    public WeaponHandler weaponHandler;

    private float timeSinceLastAttack = float.MaxValue;

    [SerializeField] private GameObject Enemy;


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

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

    private void Movement(Vector2 direction)
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

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        float weaponRotZ = Mathf.Atan2(
            TrankingEnemy().position.y - transform.position.y
            ,TrankingEnemy().position.x - transform.position.x
            ) * Mathf.Rad2Deg - 90;
        float t = 10f;

        transform.rotation = Quaternion.Lerp(
            transform.rotation, Quaternion.Euler(0,0,rotZ), Time.deltaTime * t);

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Lerp(
                weaponPivot.rotation, Quaternion.Euler(0, 0, weaponRotZ), Time.deltaTime * t);
        }
    }

    private Transform TrankingEnemy()   // 가까운 적 추적
    {
        int enemyCount = Enemy.transform.childCount;    // 적의 수
        List<Transform> enemy = new List<Transform> { };    // 적 리스트
        List<float> enemyPos = new List<float> { };     // 적 위치 리스트
        
        for (int i = 0; i < enemyCount; i++)        // 적 리스트에 추가
        {
            enemy.Add(Enemy.transform.GetChild(i));
        }

        foreach (Transform t in enemy)              // 적 위치 리스트에 추가
        {
            enemyPos.Add(Vector2.Distance(t.position, transform.position));
        }

        float closesDistance = float.MaxValue;      // 가장 가까운 거리
        Transform closestEnemy = null;              // 가장 가까운 적

        for (int i = 0; i < enemyPos.Count; i++)
        {
            if (enemyPos[i] < closesDistance)
            {
                closesDistance = enemyPos[i];
                closestEnemy = enemy[i];
            }
        }

        return closestEnemy;
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
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
        if (movementDirection == Vector2.zero)
        {
            weaponHandler.Attack();
        }
    }
}
