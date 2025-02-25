using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
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
    // ��ų�� ��ٿ�
    [SerializeField] private float cooldown = 3f;

    private bool isColldown = false;

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
        if (isColldown) return;
        StartCoroutine(SkillColldown());
        Vector2 randomPosition = RandomBoombardPosition(player.transform.position);
        GameObject boombard = Instantiate(boombardPrefabs, randomPosition, Quaternion.identity);

        boombard.transform.localScale = new Vector3(skillSize, skillSize, 1f);

        Destroy(boombard, lifetime);

    }

    // ��ų�� ��ٿ��� �Ǿ��� �� ��� �����ϰ�
    private IEnumerator SkillColldown()
    {
        isColldown = true;
        yield return new WaitForSeconds(cooldown);
        isColldown = false;
    }

    private Vector2 RandomBoombardPosition(Vector2 playerPosition)
    {

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        // minRange ~ maxRange ������ ���� ���� ��ġ�� ����
        float randomDistance = Random.Range(minRange, maxRange);
        Vector2 targetPosition = playerPosition + randomDirection * randomDistance;

        // ��ų�� �� ���� �ȳ������� ����
        float adjustX = Mathf.Clamp(targetPosition.x, mapMinBounds.x + skillSize, mapMaxBounds.x - skillSize);
        float adjustY = Mathf.Clamp(targetPosition.y, mapMinBounds.y + skillSize, mapMaxBounds.y - skillSize);

        return new Vector2(adjustX, adjustY);

    }

}
