using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public int skillLevel = 1;
    [SerializeField] protected float cooldown = 10;

    public string SkillName;
    public string RequiredItem;

    protected StatHandler statHandler;
    protected Player player;
    protected MapSizeDetecte mapSize;

    // ����ä �߻� ��ġ(���� ��ġ)
    protected Transform firePoint;

    protected bool isCooldown = false;

    protected Vector2 mapMinBounds;
    protected Vector2 mapMaxBounds;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        statHandler = GameManager.Instance.GetStatHandler();
        mapSize = GameManager.Instance.mapSize;
        firePoint = player.transform;

        // ���� ���ϴ� ��ǥ
        mapMinBounds = mapSize.GetMinBounds();
        // ���� ���� ��ǥ
        mapMaxBounds = mapSize.GetMaxBounds();

        if (player == null)
        {
            Debug.Log("�÷��̾ �������� �ʽ��ϴ�.");
        }
    }

    protected virtual void Update()
    {

    }

    public abstract void UseSkill();

    // ��ų�� ��ٿ��� �Ǿ��� �� ��� �����ϰ�
    protected IEnumerator SkillCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

}
