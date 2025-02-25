using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class BoombardmentSkill : BaseSkill
{
    // ���� ������
    [SerializeField] private GameObject boombardPrefabs;

    // ��ų�� ������ ����
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float maxRange = 6f;

    //��ų ����(���� ũ��)
    [SerializeField] private float skillSize = 0.5f;
    // ����� �ð�
    [SerializeField] private float lifetime = 1f;

    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    protected override void Start()
    {
        base.Start();
        // ���� ���ϴ� ��ǥ
        mapMinBounds = mapSize.GetMinBounds();
        // ���� ���� ��ǥ
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
        // minRange ~ maxRange ������ ���� ���� ��ġ�� ����
        float randomDistance = Random.RandomRange(minRange, maxRange);
        Vector2 targetPosition = playerPosition + randomDirection * randomDirection;

        // ��ų�� �� ���� �ȳ������� ����
        float adjustX = Mathf.Clamp(targetPosition.x, mapMinBounds.x + skillSize, mapMaxBounds.x - skillSize);
        float adjustY = Mathf.Clamp(targetPosition.y, mapMinBounds.y + skillSize, mapMaxBounds.y - skillSize);

        return new Vector2(adjustX, adjustY);

    }

}
