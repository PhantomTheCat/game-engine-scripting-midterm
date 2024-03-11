using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BriefcaseBehavior : MonoBehaviour
{
    //Properties
    [SerializeField] private Sprite openBriefcaseImage;
    [SerializeField] private Sprite closedBriefcaseImage;
    [SerializeField] private TextMeshProUGUI briefcaseText;
    [SerializeField] private GameObject briefcase;
    private Image image;
    public Image outlineImage;
    public int moneyValue;
    public bool isOpen = false;
    public bool wasChosen = false;


    //Methods
    void Awake()
    {
        //Getting the components
        image = briefcase.GetComponent<Image>();
        if (image == null) { Debug.Log("A Mistake has happened"); }
    }

    public bool Selected()
    {
        //Checking if the briefcase is
        //the player's current choice or if it is open
        if (isOpen != true && wasChosen == false)
        {
            outlineImage.color = Color.green;
            isOpen = true;
            briefcaseText.text = $"{moneyValue:C}";
            ChangeSprite();
            return true;
        }
        else { return false; }
    }

    private void ChangeSprite()
    {
        if (isOpen)
        {
            image.sprite = openBriefcaseImage;
        }
        else
        {
            image.sprite = closedBriefcaseImage;
        }
    }
}
