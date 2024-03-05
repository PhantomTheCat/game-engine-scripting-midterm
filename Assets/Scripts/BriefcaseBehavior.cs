using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefcaseBehavior : MonoBehaviour
{
    //Properties
    [SerializeField] private Sprite openBriefcaseImage;
    [SerializeField] private Sprite closedBriefcaseImage;
    private SpriteRenderer spriteRenderer;
    public int moneyValue;


    //Methods
    void Start()
    {
        //Getting the component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

}
