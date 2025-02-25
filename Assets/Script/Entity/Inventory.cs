using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    Player player;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public bool HasItem(Item itemName)
    {
        return items.Contains(itemName);
    }

    public void Additem(Item item, Player player)
    {
        // items ����Ʈ�� itemName�� �ش��ϴ� �������� ���ٸ� ������ �߰�
        if (!items.Contains(item))
        {
            items.Add(item);
            item.ApplyEffect(player);
            player.skillManager.ResgisterSkills(item);
        }
    }

    public void Additem(Item item)
    {
        // items ����Ʈ�� itemName�� �ش��ϴ� �������� ���ٸ� ������ �߰�
        if (!items.Contains(item))
        {
            items.Add(item);
            item.ApplyEffect(player);
            player.skillManager.ResgisterSkills(item);
        }
    }

}
