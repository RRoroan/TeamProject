using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    // ������ ��ų
    public BaseSkill skill;

    Player player;

    protected StatHandler statHandler;

    protected void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected void Start()
    {
        statHandler = GameManager.Instance.GetStatHandler();
    }
    

    public virtual void ApplyEffect(Player player)
    {
        skill = GetComponent<BaseSkill>();
        if (skill != null)
        {
            player.skillManager.AddSkill(skill);
            Debug.Log("��ų�� ��ϵ�");
        }
        return;
    }
}
