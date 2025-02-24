using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera _camera;

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2 (horizontal, vertical).normalized;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;

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
        }

        Debug.DrawLine(transform.position, closestEnemy.transform.position);
    }
}
