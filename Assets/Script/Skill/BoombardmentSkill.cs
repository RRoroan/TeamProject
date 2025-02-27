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
    [Header("���� ��ų ����")]
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float maxRange = 6f;

    //��ų ����(���� ũ��)
    [SerializeField] private float skillSize = 0.5f;
    // ����� �ð�
    [SerializeField] private float lifetime = 1f;
    [Header("Ÿ��")]
    [SerializeField] private LayerMask enemyLayer;





    protected override void Start()
    {
        base.Start();
    }

    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} ������Ʈ�� ��Ȱ��ȭ ���¿��� ���� �Ұ���!");
            return;
        }

        Vector2 randomPosition = RandomBoombardPosition(player.transform.position);
        GameObject boombard = Instantiate(boombardPrefabs, randomPosition, Quaternion.identity);
        boombard.transform.localScale = new Vector3(skillSize, skillSize, 1f);

        BoombardmentSkillExplosion explosion = boombard.GetComponent<BoombardmentSkillExplosion>();
        if (explosion != null)
        {
            explosion.Init(skillSize, enemyLayer);
        }

        Destroy(boombard, lifetime);

    }

    private Vector2 RandomBoombardPosition(Vector2 playerPosition)
    {
        Vector2 targetPosition;
        int maxAttempts = 100; //�ݺ�Ƚ���� ������ �ɱ�
        int attempts = 0;

        do
        {
            attempts++;
            // minRange ~ maxRange ������ ���� ���� ��ġ�� ����
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minRange, maxRange);
            targetPosition = playerPosition + randomDirection * randomDistance;

            if (attempts > maxAttempts)
            {
                Debug.LogError("BoombardmentSkill : ��ġ�� ã�� ����");
                break;
            }
        }
        while (!isInMapBounds(targetPosition));

        return targetPosition;

    }

    private bool isInMapBounds(Vector2 position)
    {
        // ��ų�� �� ���� �ȳ������� ����
        return (position.x >= mapMinBounds.x + skillSize) && (position.x <= mapMaxBounds.x - skillSize)
            && (position.y >= mapMinBounds.y + skillSize) && (position.y <= mapMaxBounds.y - skillSize);
    }

}
