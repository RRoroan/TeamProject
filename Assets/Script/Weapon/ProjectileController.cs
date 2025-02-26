using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;
    private int bounceCount = 0;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    ProjectileManager projectileManager;
    private AnimationHandler animationHandler;
    private ProjectileAnimationHandler projectileAnimationHandler;
    private float destructionDelay = 0.5f;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
        animationHandler = GetComponent<AnimationHandler>();
        projectileAnimationHandler = GetComponent<ProjectileAnimationHandler>();
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
    {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return; 
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            if (bounceCount < rangeWeaponHandler.MaxBounces)
            {
                Vector2 normal = collision.contacts[0].normal;
                direction = Vector2.Reflect(direction, normal);

                bounceCount++;
            }
            else
            {
                if (projectileAnimationHandler != null)
                {
                    projectileAnimationHandler.PlayDestroyAnimation();
                }
                else
                {
                    DestroyProjectile(collision.contacts[0].normal);
                }
            }
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.gameObject.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-5);
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.gameObject.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }
            if(projectileAnimationHandler != null)
            {
                projectileAnimationHandler.PlayDestroyAnimation();
            }
            else
            {
                DestroyProjectile(collision.contacts[0].normal);
            }
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        transform.right = this.direction;

        if (this.direction.x < 0)
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }

        isReady = true;

    }

    private void DestroyProjectile(Vector3 position)
    {
        Destroy(this.gameObject);
    }
}
