using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    [SerializeField] private RectTransform front;

    private Transform playerTransform;
    private Text hpText;
    private ResourceController resourceController;

    void Start()
    {
        playerTransform = transform.parent;
        resourceController = GetComponentInParent<ResourceController>();
        hpText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0, .8f, 0);
        transform.rotation = Quaternion.identity;

        float currentHealth = resourceController.CurrentHealth; 
        float MaxHealth = resourceController.MaxHealth;
        
        float hpBarScale = currentHealth / MaxHealth;
        if (hpBarScale >= 0 && hpBarScale <= 1)
        {
            front.localScale = Vector3.Lerp(
                front.localScale, new Vector3(hpBarScale, 1, 1), Time.deltaTime * 10f);

            hpText.text = currentHealth.ToString();
        }
    }
}
