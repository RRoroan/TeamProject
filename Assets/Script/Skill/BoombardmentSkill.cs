using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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

    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    protected override void Start()
    {
        base.Start();
        // 맵의 좌하단 좌표
        mapMinBounds = mapSize.GetMinBounds();
        // 맵의 우상단 좌표
        mapMaxBounds = mapSize.GetMaxBounds();
    }

    public override void UseSkill()
    {
        Vector2 randomPosition = RandomBoombardPosition(player.transform.position);
        Instantiate(boombardPrefabs, randomPosition, Quaternion.identity);

        Destroy(boombardPrefabs, lifetime);

    }

    private Vector2 RandomBoombardPosition(Vector2 playerPosition)
    {

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        // minRange ~ maxRange 사이의 랜덤 포격 위치를 설정
        float randomDistance = Random.RandomRange(minRange, maxRange);
        Vector2 targetPosition = playerPosition + randomDirection * randomDirection;

        // 스킬이 맵 밖을 안나가도록 조정
        float adjustX = Mathf.Clamp(targetPosition.x, mapMinBounds.x + skillSize, mapMaxBounds.x - skillSize);
        float adjustY = Mathf.Clamp(targetPosition.y, mapMinBounds.y + skillSize, mapMaxBounds.y - skillSize);

        return new Vector2(adjustX, adjustY);

    }

}
