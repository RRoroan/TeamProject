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
        // items ����Ʈ�� itemName�� �ش��ϴ� �������� ���ٸ� ������ �߰�
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
        }
    }

}
