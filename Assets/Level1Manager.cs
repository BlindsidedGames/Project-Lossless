using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Oracle;

public class Level1Manager : MonoBehaviour
{
    private Level1 saveData => oracle.saveData.level1;

    [SerializeField] private Slider castBar;
    
    private float castTime;
    private float lure1CastTime = 5;
    private float lure23CastTime = 4;
    private float lure4CastTime = 3;
    private float lure5CastTime = 2;
    private float lure6CastTime = 1;
    void Update()
    {
        CastBar();
    }

    private void CastBar()
    {
        castTime += Time.deltaTime;
        var castTimeMax = 0f;
        switch (saveData.currentLure)
        {
            case Level1Lures.First:
                castTimeMax = lure1CastTime;
                break;
            case Level1Lures.Second:
                castTimeMax = lure23CastTime;
                break;
            case Level1Lures.Third:
                castTimeMax = lure23CastTime;
                break;
            case Level1Lures.Fourth:
                castTimeMax = lure4CastTime;
                break;
            case Level1Lures.Fifth:
                castTimeMax = lure5CastTime;
                break;
            case Level1Lures.Sixth:
                castTimeMax = lure6CastTime;
                break;
        }
        SetProgressBar(castTimeMax);
        if (castTime >= castTimeMax)
        {
            castTime -= castTimeMax;
            Catch();
        }
    }

    private void SetProgressBar(float currentCastTime)
    {
        castBar.value =  castTime/currentCastTime ;
    }
    
    private void Catch()
    {
        Roll((int)saveData.currentLure);
    }

    #region Rolling

    private void RollJunk()
    {
        var random = Random.Range(1,6);
        switch (random)
        {
            case 1:
                saveData.boot++;
                break;
            case 2:
                saveData.can++;
                break;
            case 3:
                saveData.tyre++;
                break;
            case 4:
                saveData.plasticBottle++;
                break;
            case 5:
                saveData.plant++;
                break;
        }
    }

    private void RollFish()
    {
        var random = Random.Range(1,12-(int)saveData.currentLure/5);
        switch (random)
        {
            case 1:
                saveData.shrimp++;
                saveData.cash += 10;
                break;
            case 2:
                saveData.anchovies++;
                saveData.cash += 25;
                break;
            case 3:
                saveData.cod++;
                saveData.cash += 50;
                break;
            case 4:
                saveData.flounder++;
                saveData.cash += 80;
                break;
            case 5:
                saveData.halibut++;
                saveData.cash += 100;
                break;
            case 6:
                saveData.trout++;
                saveData.cash += 300;
                break;
            case 7:
                saveData.sardines++;
                saveData.cash += 400;
                break;
            case 8:
                saveData.herring++;
                saveData.cash += 700;
                break;
            case 9:
                saveData.salmon++;
                saveData.cash += 800;
                break;
            case 10:
                saveData.tuna++;
                saveData.cash += 1000;
                break;
            default:
                Debug.Log("InvalidRoll");
                break;
        }
    }

    private void Roll(int junkChance)
    {
        switch (FishOrJunk(junkChance))
        {
            case true :
                RollJunk();
                break;
            default:
                RollFish();
                break;
        }
    }

    private bool FishOrJunk(float junkChance)
    {
        var random = Random.Range(1, 101);
        return random <= junkChance;
    }

    #endregion
}
