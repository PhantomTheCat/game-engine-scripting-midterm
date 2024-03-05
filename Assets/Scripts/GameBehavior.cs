using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    //Properties
    [SerializeField]
    private int[] briefcaseValues = new int[10]
    {
        1, 5, 10, 100, 1000, 10000, 50000, 100000, 500000, 100000000
    };

    [SerializeField]
    private int[] randomizedBriefcaseValues = new int[10];

    private int[] correlatingBriefcaseValues = new int[10]
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10
    };


    //Methods
    void Awake()
    {
        //Randomizing the values of the briefcases
        //Because we want to give a random value to each briefcase
        for (int i = 0; i < briefcaseValues.Length; i++)
        {
            //Getting random number
            int randomIndex = Random.Range(0, briefcaseValues.Length);

            //Verify random number (Loop it until we get it right)


            //Set that value

            /*
            if (correlatingBriefcaseValues[randomIndex] != 0)
            {
                randomizedBriefcaseValues[i] = briefcaseValues[randomIndex];

                correlatingBriefcaseValues[randomIndex] = 0;
            }*/
        }

    }

    void Update()
    {
        
    }

    public void SelectBriefcase()
    {

    }
}
