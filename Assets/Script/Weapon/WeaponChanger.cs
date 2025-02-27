using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public List<WeaponHandler> weaponPrefabs; 
    private int currentOption = 0;

    private void Start()
    {
    }

    public void NextOption()
    {
        currentOption++;
        if (currentOption >= weaponPrefabs.Count)
        {
            currentOption = 0; 
        }

        // Equip the selected weapon
        PlayerController playerController = GetComponent<PlayerController>(); // Get the PlayerController component
        if (playerController != null)
        {
            playerController.EquipWeapon(weaponPrefabs[currentOption]);
        }
    }
}
