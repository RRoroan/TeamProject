using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera _camera;

    private GameManager gameManager;
    public AudioClip moveSoundClip;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        GameObject[] enemyType1 = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyType2 = GameObject.FindGameObjectsWithTag("Enemy2");
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;

        List<GameObject> enemies = new List<GameObject>(enemyType1);
        enemies.AddRange(enemyType2);

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        if (closestEnemy != null)
        {
            Vector2 directionToTarget = closestEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
            weaponPivot.rotation = Quaternion.Lerp(weaponPivot.rotation, targetRotation, Time.deltaTime * 10f);
            if (Quaternion.Angle(weaponPivot.rotation, targetRotation) < 10f)  // 오차 범위 1도로 설정
            {
                readytoAttack = true;
            }
            else
            {
                readytoAttack = false;
            }
            lookDirection = directionToTarget;
        }
        else
        {
            readytoAttack = false;
        }
    }
    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
        if (moveSoundClip)
        {
            SoundManager.PlayClip(moveSoundClip);
        }
    }
}
