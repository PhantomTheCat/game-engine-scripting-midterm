using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BriefcaseBehavior : MonoBehaviour
{
    //Properties
    [SerializeField] private Sprite openBriefcaseImage;
    [SerializeField] private Sprite closedBriefcaseImage;
    [SerializeField] private TextMeshProUGUI briefcaseText;
    [SerializeField] private GameObject briefcase;
    private SpriteRenderer spriteRenderer;
    public int moneyValue;
    public bool isOpen = false;


    //Methods
    void Awake()
    {
        //Getting the component
        spriteRenderer = briefcase.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = openBriefcaseImage;
        }
        else
        {
            spriteRenderer.sprite = closedBriefcaseImage;
        }
    }
}
