using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    //public string itemName;
    //public string description;
    //// 아이템 스킬
    //public BaseSkill skill;

    //protected Player player;

    //protected StatHandler statHandler;

    //public void Awake()
    //{
    //    player = FindObjectOfType<Player>();
    //}

    //protected virtual void Start()
    //{
    //    statHandler = GameManager.Instance.GetStatHandler();
    //}
    

    //public virtual void ApplyEffect(Player player)
    //{
    //    skill = GetComponent<BaseSkill>();

    //    if (skill != null)
    //    {
    //        // 중복 등록 방지
    //        if (!player.skillManager.HasSkill(skill))
    //        {
    //            player.skillManager.ResgisterSkills(this);
    //            Debug.Log($"{itemName} 효과 적용됨");
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"{itemName} 스킬이 이미 등록됨");
    //        }
    //    }
    //}
}
