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
        // 마우스 위치에서 UI 오브젝트 체크
        if (IsMouseOverUI())
        {
            changecolor.color = new Color(0.5f, 0.5f, 0.5f, 1f); // 어둡게
            if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭 시
            {
                TooltipUpdate(decKey); // 툴팁 변경
            }
        }
        else
        {
            changecolor.color = Color.white; // 원래 색상 복구
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
                tooltip.text = "플레이어의 공격력을 증가시킨다.";
                break;
            case 1:
                tooltip.text = "플레이어의 공격속도를 증가시킨다.";
                break;
            case 2:
                tooltip.text = "플레이어의 이동속도를 증가시킨다.";
                break;
            case 3:
                tooltip.text = "플레이어의 최대체력을 증가시킨다.";
                break;
            case 4:
                tooltip.text = "플레이어의 체력을 증가시킨다.";
                break;
            case 5:
                tooltip.text = "플레이어의 투사체를 증가시킨다.";
                break;
            case 6:
                tooltip.text = "플레이어의 투사체가 관통합니다.";
                break;
            case 7:
                tooltip.text = "플레이어의 투사체가 도탄됩니다.";
                break;
            default: tooltip.text = null; break;
        }
        
    }
}
