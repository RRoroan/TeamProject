using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    protected StatHandler statHandler = GameManager.Instance.GetStatHandler();
    

    public abstract void ApplyEffect(StatHandler statHandler);
}
