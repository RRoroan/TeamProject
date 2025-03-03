using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class BoombardmentSkill : BaseSkill
{
    // 포격 프리팹
    [SerializeField] private GameObject boombardPrefabs;


    [Header("타겟")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("탄두 설정")]
    [SerializeField] private float arrivalTime = 2f;

    // 스킬이 떨어질 범위
    [Header("포격 스킬 설정")]
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float maxRange = 8f;

    //스킬 범위(포격 크기)
    [SerializeField] private float skillSize = 1.5f;





    protected override void Start()
    {
        base.Start();
    }

    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} 오브젝트가 비활성화 상태여서 실행 불가능!");
            return;
        }

        Vector2 randomPosition = RandomBoombardPosition(playerController.transform.position);
        Vector2 createPosition = new Vector2(randomPosition.x + Random.Range(mapMinBounds.x - 5, mapMaxBounds.x + 5)
            , randomPosition.y + Random.Range(mapMaxBounds.y + 3, mapMaxBounds.y + 6));
        GameObject boombard = Instantiate(boombardPrefabs, createPosition, Quaternion.identity);
        boombard.transform.localScale = new Vector3(skillSize, skillSize, 1f);

        BoombardmentSkillExplosion explosion = boombard.GetComponent<BoombardmentSkillExplosion>();
        if (explosion != null)
        {
            explosion.Init(damage, skillSize, enemyLayer, arrivalTime, randomPosition);
            explosion.transform.rotation = explosion.RotateToTarget();
        }

    }


    private Vector2 RandomBoombardPosition(Vector2 playerPosition)
    {
        Vector2 targetPosition;
        int maxAttempts = 100; //반복횟수에 제한을 걸기
        int attempts = 0;

        do
        {
            attempts++;
            // minRange ~ maxRange 사이의 랜덤 포격 위치를 설정
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minRange, maxRange);
            targetPosition = playerPosition + randomDirection * randomDistance;

            if (attempts > maxAttempts)
            {
                Debug.LogError("BoombardmentSkill : 위치를 찾지 못함");
                break;
            }
        }
        while (!isInMapBounds(targetPosition));

        return targetPosition;

    }

    private bool isInMapBounds(Vector2 position)
    {
        // 스킬이 맵 밖을 안나가도록 조정
        return (position.x >= mapMinBounds.x + skillSize) && (position.x <= mapMaxBounds.x - skillSize)
            && (position.y >= mapMinBounds.y + skillSize) && (position.y <= mapMaxBounds.y - skillSize);
    }

    public override void SkillLevelUp()
    {
        base.SkillLevelUp();
        if (skillLevel % 2 == 1)
        {
            if (skillSize < 5)
            {
                skillSize += 0.25f;
            }
            else
            {
                damage++;
            }
        }
        else
        {
            if (cooldown > 2)
            {

                cooldown = Mathf.Max(cooldown - 0.5f, 2f);

            }
            else
            {
                damage++;
            }
        }
        damage += 2;

    }

}
