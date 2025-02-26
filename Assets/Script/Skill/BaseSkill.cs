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

    // 투사채 발사 위치(시작 위치)
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

        // 맵의 좌하단 좌표
        mapMinBounds = mapSize.GetMinBounds();
        // 맵의 우상단 좌표
        mapMaxBounds = mapSize.GetMaxBounds();

        if (player == null)
        {
            Debug.Log("플레이어가 존재하지 않습니다.");
        }
    }

    protected virtual void Update()
    {

    }

    public abstract void UseSkill();

    // 스킬의 쿨다운이 되었을 때 사용 가능하게
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
