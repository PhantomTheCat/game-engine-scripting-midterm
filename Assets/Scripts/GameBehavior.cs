using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI valuesLeftText;

    [SerializeField]
    private GameObject[] briefcaseObjects = new GameObject[10];

    //Methods
    void Awake()
    {
        RandomizeBriefcaseValues();
        GiveBriefcasesValues();
    }

    void Update()
    {
        ValuesLeft();
    }

    public void SelectBriefcase(int arrayNumber)
    {

    }

    private void RandomizeBriefcaseValues()
    {
        //Randomizing the values of the briefcases
        //Because we want to give a random value to each briefcase
        //(Still want to only have one instance per slot)
        for (int i = 0; i < briefcaseValues.Length; i++)
        {
            //Getting random number
            int randomIndex = Random.Range(0, briefcaseValues.Length);

            //Verify random number
            if (correlatingBriefcaseValues[randomIndex] != 0)
            {
                randomizedBriefcaseValues[i] = briefcaseValues[randomIndex];

                correlatingBriefcaseValues[randomIndex] = 0;
            }
            else
            {
                //If the randomized value is one we already have
                //We will redo this i until we get one that works
                i--;
            }
        }

    }

    private void ValuesLeft()
    {
        //Filling our values left text with the values left
        valuesLeftText.text = GetArrayValues();
    }

    private string GetArrayValues()
    {
        string output = "";
        int index = 1;
        int numberOfSpaces = 7;

        foreach (int i in briefcaseValues)
        {
            //Searching the randomized values for the base values
            foreach (int j in randomizedBriefcaseValues)
            {
                //Comparing them
                if (i == j && index == 1)
                {
                    //First one in row = tab
                    output += $"{i}         ";

                    //It will have more spaces which will lessen over time
                    for (int m = 0; m < numberOfSpaces; m++)
                    {
                        output += " ";
                    }

                    numberOfSpaces--;
                    index++;
                }
                else if (i == j && index == 2)
                {
                    //Second one in row = new line
                    output += $"{i}\n";
                    index--;
                }
            }
        }

        return output;
    }

    private void GiveBriefcasesValues()
    {
        int arrayIndex = 0;

        //Giving each briefcase a value from our randomized array
        foreach (GameObject briefcase in briefcaseObjects)
        {
            BriefcaseBehavior briefcaseBehavior = briefcase.GetComponent<BriefcaseBehavior>();
            briefcaseBehavior.moneyValue = randomizedBriefcaseValues[arrayIndex];
            arrayIndex++;
        }
    }
}
