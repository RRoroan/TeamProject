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

    // 스킬이 떨어질 범위
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float maxRange = 6f;

    //스킬 범위(포격 크기)
    [SerializeField] private float skillSize = 0.5f;
    // 사라질 시간
    [SerializeField] private float lifetime = 1f;


    private bool isColldown = false;

    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    protected override void Start()
    {
        base.Start();
        // 맵의 좌하단 좌표
        mapMinBounds = mapSize.GetMinBounds();
        // 맵의 우상단 좌표
        mapMaxBounds = mapSize.GetMaxBounds();

        cooldown = 15f;
    }

    public override void UseSkill()
    {
        if (isColldown) return;
        StartCoroutine(SkillCooldown());
        Vector2 randomPosition = RandomBoombardPosition(player.transform.position);
        GameObject boombard = Instantiate(boombardPrefabs, randomPosition, Quaternion.identity);

        boombard.transform.localScale = new Vector3(skillSize, skillSize, 1f);

        Destroy(boombard, lifetime);

    }

    private Vector2 RandomBoombardPosition(Vector2 playerPosition)
    {
        Vector2 targetPosition;

        do
        {

            // minRange ~ maxRange 사이의 랜덤 포격 위치를 설정
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minRange, maxRange);
            targetPosition = playerPosition + randomDirection * randomDistance;
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

}
