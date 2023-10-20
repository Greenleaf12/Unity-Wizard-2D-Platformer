using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarNew : MonoBehaviour
{

    private Image barImage;

    private float barMaskWidth;

    public float manaAmount = 100f;
    public float manaRegenAmount = 1f;
    public float manaNewRegenAmount = 1f;

    public const int MANA_MAX = 50;

    private RectTransform edgeRectTransform;
    private RectTransform barMaskRectTransform;
    private RawImage barRawImage;

    private void Awake()

    {
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;

        edgeRectTransform = transform.Find("barMask").Find("BarEdge").GetComponent<RectTransform>();

}

    private void Update()
    {
        
        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= .1f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = GetManaNormalized() * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        manaAmount += manaRegenAmount * Time.deltaTime;
        manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX);

        edgeRectTransform.anchoredPosition = new Vector2(GetManaNormalized() * barMaskWidth+25, 0); // 965 / 475

        if (Input.GetKeyDown(KeyCode.Space) && manaAmount > 5)
        {
            FindObjectOfType<PlayerMovement>().Kick();
            SpendMana(5);
        }
        
    }

    public void AddMana(int amount)
    {
        manaAmount += amount;
    }

    public void SpendMana(int amount)
    {
        manaAmount -= amount;
    }

    public float GetManaNormalized()
    {
        return manaAmount / MANA_MAX;
    }

}
