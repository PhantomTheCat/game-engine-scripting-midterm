using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Project: Midterm Project - Deal Or No Deal
 * By Aiden McCallum
 * Credits:
 *      - Help gotten from our work from in-class demos for Game Engine Scripting
 *      - Sounds gotten from FreeSound.org
*/

public class GameBehavior : MonoBehaviour
{
    //Properties
    [Header("Lists")]
    [SerializeField] private int[] briefcaseValues = new int[10]
    {
        1, 5, 10, 100, 1000, 10000, 50000, 100000, 500000, 1000000
    };

    private int[] correlatingBriefcaseValues = new int[10]
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10
    };

    [SerializeField] private GameObject[] briefcaseObjects = new GameObject[10];
    [SerializeField] private int[] randomizedBriefcaseValues = new int[10];
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI valuesLeftText;
    [SerializeField] private TextMeshProUGUI choiceText;
    [SerializeField] private TextMeshProUGUI endText;
    [Header("Buttons")]
    [SerializeField] private Button dealButton;
    [SerializeField] private Button noDealButton;
    [Header("Numbers")]
    [SerializeField] private GameObject caseNumbers;
    private List<BriefcaseBehavior> briefcaseBehaviors = new List<BriefcaseBehavior>();
    private int amountSelected = 0;
    private int amountOfBriefcasesLeft = 10;
    private int amountWon = 0;

    //Methods
    void Awake()
    {
        //Randomizing and giving each briefcase values
        RandomizeBriefcaseValues();
        GiveBriefcasesValues();

        amountOfBriefcasesLeft = briefcaseObjects.Length;

        //Getting a list of all the Briefcases scripts
        foreach (GameObject gameObject in briefcaseObjects)
        {
            BriefcaseBehavior briefcase = gameObject.GetComponent<BriefcaseBehavior>();
            briefcaseBehaviors.Add(briefcase);
        }
    }

    void Update()
    {
        GetMoneyValuesInPlay();
    }

    public void SelectBriefcase(int number)
    {
        //Stopping them from picking this if the designated amount has already been reached
        if (amountSelected == 2) { return; }

        int arrayNumber = number - 1;

        //Calling on this method when player's click on a briefcase
        BriefcaseBehavior briefcase = briefcaseObjects[arrayNumber].GetComponent<BriefcaseBehavior>();

        //For start of game
        if (amountOfBriefcasesLeft == 10)
        {
            briefcase.wasChosen = true;
            choiceText.text = $"You chose briefcase #{number}! Now please choose 2 cases to discard...";
            briefcase.outlineImage.color = Color.red;
            amountOfBriefcasesLeft--;
        }
        //Checking if in range for the end of game (Cancels out default approach)
        else if (amountOfBriefcasesLeft == 1)
        {
            if (briefcase.wasChosen) //They chose their one
            {
                choiceText.text = $"You chose your own briefcase!";
            }
            else if (!briefcase.wasChosen) //Chose the other one
            {
                choiceText.text = $"You chose the other briefcase!";
            }

            //Playing the chime sound when opening a briefcase
            SoundManager.PlaySound(SoundManager.SoundType.CHING);

            amountWon = briefcase.moneyValue;
            EndGame();
        }
        //For the default approach
        else if (briefcase.Selected() && amountSelected <= 2)
        {
            amountSelected++;
            amountOfBriefcasesLeft--;
            choiceText.text = $"You opened a briefcase worth {briefcase.moneyValue:C}";

            //Playing the chime sound when opening a briefcase
            SoundManager.PlaySound(SoundManager.SoundType.CHING);

            if (amountSelected == 2)
            {
                Invoke("SetUpOffer", 2f);
            }
        }
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

    private void GetMoneyValuesInPlay()
    {
        //Filling our values left text with the money values left in play
        valuesLeftText.text = GetArrayValues();
    }

    private string GetArrayValues()
    {
        //Searches the gameObjects to see which array values from
        //our base array are in play, and formats them into text as a string
        string output = "";
        int index = 1;
        int numberOfSpaces = 10;
        List<int> moneyBriefcaseValues = new List<int>();

        //Populating a list with money values from the briefcases
        foreach (BriefcaseBehavior briefcase in briefcaseBehaviors)
        {
            if (briefcase.isOpen == false)
            {
                moneyBriefcaseValues.Add(briefcase.moneyValue);
            }
        }

        foreach (int i in briefcaseValues)
        {
            //Searching the randomized values for the base values
            foreach (int j in moneyBriefcaseValues)
            {
                //Comparing them
                if (i == j && index == 1)
                {
                    //First one in row = tab
                    output += $"{i:C}         ";

                    //It will have more spaces which will lessen over time
                    for (int m = 0; m < numberOfSpaces; m++)
                    {
                        output += " ";
                    }

                    numberOfSpaces -= 2;
                    index++;
                }
                else if (i == j && index == 2)
                {
                    //Second one in row = new line
                    output += $"{i:C}\n";
                    index--;
                }
            }
        }

        return output;
    }

    public void DealPressed()
    {
        //Linked to our DealButton
        TurnOnOrOffDealButtons(false);
        EndGame();
    }

    public void NoDealPressed()
    {
        //Linked to our NoDealButton
        amountSelected = 0;
        TurnOnOrOffBriefcases(true);
        TurnOnOrOffDealButtons(false);

        //Playing the rejection sound
        SoundManager.PlaySound(SoundManager.SoundType.NODEAL);

        //Different prompts depending on if we are at end or not
        if (amountOfBriefcasesLeft == 1)
        {
            choiceText.text = "Chose to open up your own briefcase or the other one...";
        }
        else
        {
            choiceText.text = "Open 2 more briefcases";
        }
    }

    private void SetUpOffer()
    {
        TurnOnOrOffBriefcases(false);
        choiceText.text = GetOffer();
    }

    private string GetOffer()
    {
        string output = "Your current offer is: ";
        List<int> moneyValues = new List<int>();
        int totalValue = 0;
        int priceDecreaseModifier = 10;

        //Having the amount selected be set back to 0,
        //So we can access our SelectBriefcase from buttons again
        amountSelected = 0;

        //Populating a list with money values from the briefcases
        foreach (BriefcaseBehavior briefcase in briefcaseBehaviors)
        {
            if (briefcase.isOpen == false)
            {
                moneyValues.Add(briefcase.moneyValue);
            }
        }

        //Getting all amounts
        foreach (int i in moneyValues)
        {
            totalValue += i;
        }

        //Getting the mean of the briefcases left for the offer
        if (totalValue > 1000000) //$1,000,000
        {
            priceDecreaseModifier = 4;
        }
        else if (totalValue < 100000) //$100,000
        {
            priceDecreaseModifier = 12;
        }
        totalValue = totalValue / (amountOfBriefcasesLeft + priceDecreaseModifier);
        amountWon = totalValue;
        output += $"{totalValue:C}";

        TurnOnOrOffDealButtons(true);

        return output;
    }

    private void EndGame()
    {
        TurnOnOrOffBriefcases(false);

        //Turning on sound for cheering as game ends
        SoundManager.PlaySound(SoundManager.SoundType.CROWDCHEER);

        endText.gameObject.SetActive(true);
        endText.text = $"You won {amountWon:C}";
    }

    private void TurnOnOrOffBriefcases(bool isOn)
    {
        foreach (GameObject gameObject in briefcaseObjects)
        {
            gameObject.SetActive(isOn);
        }
        caseNumbers.gameObject.SetActive(isOn);
    }

    private void TurnOnOrOffDealButtons(bool isOn)
    {
        dealButton.gameObject.SetActive(isOn);
        noDealButton.gameObject.SetActive(isOn);
    }
}
