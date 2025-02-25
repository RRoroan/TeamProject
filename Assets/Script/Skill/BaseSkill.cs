using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string SkillName;
    public string RequiredItem;

    protected StatHandler statHandler;
    protected Player player;
    protected MapSizeDetecte mapSize;

    // ����ä �߻� ��ġ(���� ��ġ)
    protected Transform firePoint;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        statHandler = GameManager.Instance.GetStatHandler();
        mapSize = GameManager.Instance.mapSize;
        firePoint = player.transform;
    }

    public abstract void UseSkill();
}
