using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Mana mana;
    private Image barImage;
    private float barMaskWidth;
    public Mana manaAmount;
    public float manaTotal;

    private RectTransform edgeRectTransform;
    private RectTransform barMaskRectTransform;
    private RawImage barRawImage;

    private void Awake()
    {
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;

        mana = new Mana();

        edgeRectTransform = transform.Find("barMask").Find("BarEdge").GetComponent<RectTransform>();
    }

    private void Update()
    {
        mana.Update();

        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= .1f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = mana.GetManaNormalized() * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        manaTotal = mana.manaAmount;
        
        edgeRectTransform.anchoredPosition = new Vector2(mana.GetManaNormalized() * barMaskWidth+25, 0); // 965 / 475
    }

    public class Mana
    { 
        public const int MANA_MAX = 50;
        public float manaAmount;
        public float manaRegenAmount;

        public Mana()            
        {
            manaAmount = 100;
            manaRegenAmount = 2;
        }

        public void Update()
        {
            manaAmount += manaRegenAmount * Time.deltaTime;
            manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX);

            if (Input.GetKeyDown(KeyCode.Space) && manaAmount > 5)
            {
                FindObjectOfType<PlayerMovement>().Kick();
                SpendMana(5);
            }
            if (Input.GetButton("Run") && manaAmount > 0)
            {
                manaRegenAmount = -10f;
            }
            else {
                manaRegenAmount = 2f;
            }
        }
        public void SpendMana (int amount)
        {
                manaAmount -= amount;
        }

        public void AddMana(int amount)
        {
            manaAmount += amount;
        }

        public float GetManaNormalized()
        {
            return manaAmount / MANA_MAX;
        }   
    }     
}
