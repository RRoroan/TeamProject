using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public SkillManager skillManager;

    private void Awake()
    {
        skillManager = GetComponentInChildren<SkillManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            inventory.Additem(item, this);
            Destroy(collision.gameObject);
        }

    }
}
