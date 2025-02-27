using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDec : MonoBehaviour
{
    [SerializeField]
    public int decKey;
    public Image changecolor;
    public Text tooltip;

    private void Start()
    {
        changecolor = GetComponent<Image>();
        if (tooltip == null)
        {
            tooltip = GameObject.Find("WeaponDecText").GetComponent<Text>();
        }

    }

    private void Update()
    {
        // ���콺 ��ġ���� UI ������Ʈ üũ
        if (IsMouseOverUI())
        {
            changecolor.color = new Color(0.5f, 0.5f, 0.5f, 1f); // ��Ӱ�
            if (Input.GetMouseButtonDown(0)) // ���� Ŭ�� ��
            {
                TooltipUpdate(decKey); // ���� ����
            }
        }
        else
        {
            changecolor.color = Color.white; // ���� ���� ����
        }
    }

    private bool IsMouseOverUI()
    {
        Vector2 localMousePos;
        RectTransform rectTransform = changecolor.rectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localMousePos);

        return rectTransform.rect.Contains(localMousePos);
    }

    private void TooltipUpdate(int input)
    {
        switch (input)
        {
            case 0:
                tooltip.text = "�÷��̾��� ���ݷ��� ������Ų��.";
                break;
            case 1:
                tooltip.text = "�÷��̾��� ���ݼӵ��� ������Ų��.";
                break;
            case 2:
                tooltip.text = "�÷��̾��� �̵��ӵ��� ������Ų��.";
                break;
            case 3:
                tooltip.text = "�÷��̾��� �ִ�ü���� ������Ų��.";
                break;
            case 4:
                tooltip.text = "�÷��̾��� ü���� ������Ų��.";
                break;
            case 5:
                tooltip.text = "�÷��̾��� ����ü�� ������Ų��.";
                break;
            case 6:
                tooltip.text = "�÷��̾��� ����ü�� �����մϴ�.";
                break;
            case 7:
                tooltip.text = "�÷��̾��� ����ü�� ��ź�˴ϴ�.";
                break;
            default: tooltip.text = null; break;
        }
        
    }
}
