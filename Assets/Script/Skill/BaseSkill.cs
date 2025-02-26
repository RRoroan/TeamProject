using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField] protected float cooldown = 10;

    public string SkillName;
    public string RequiredItem;

    protected StatHandler statHandler;
    protected Player player;
    protected MapSizeDetecte mapSize;

    // ����ä �߻� ��ġ(���� ��ġ)
    protected Transform firePoint;

    protected bool isCooldown = false;

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

    // ��ų�� ��ٿ��� �Ǿ��� �� ��� �����ϰ�
    protected IEnumerator SkillCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }
}
