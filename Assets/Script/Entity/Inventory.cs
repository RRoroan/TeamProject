using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> items = new List<string>();

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void Additem(string itemName)
    {
        // items 리스트에 itemName에 해당하는 아이템이 없다면 아이템 추가
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
        }
    }

}
