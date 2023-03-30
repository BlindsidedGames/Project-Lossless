using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Oracle;
using Random = UnityEngine.Random;

public class Level1Manager : MonoBehaviour
{
    private Level1 saveData => oracle.saveData.level1;

    [SerializeField] private Slider castBar;
    [SerializeField] private MyButton lurePurchaseButton;
    [SerializeField] private Image lurePurchaseImage;
    [SerializeField] private TMP_Text lurePurchaseCost;

    private float castTime;

    private void Start()
    {
        lurePurchaseImage.sprite = GetLureType((int)saveData.currentLure + 1).lureImage;
        lurePurchaseCost.text = $"${CalcUtils.FormatNumber(GetLureType((int)saveData.currentLure + 1).lureCost)}";
        lurePurchaseButton.onClick.AddListener(PurchaseLure);
    }

    private void PurchaseLure()
    {
    }

    private void Update()
    {
        CastBar();
    }

    private void CastBar()
    {
        castTime += Time.deltaTime;
        var castTimeMax = GetLureType((int)saveData.currentLure).lureCastTime;


        SetProgressBar(castTimeMax);
        if (castTime >= castTimeMax)
        {
            castTime -= castTimeMax;
            Catch();
        }
    }

    private Level1Data.Lure GetLureType(int lure)
    {
        var lureData = new Level1Data.Lure();
        switch (lure)
        {
            case 1:
                lureData = oracle.level1Data.lure1;
                break;
            case 2:
                lureData = oracle.level1Data.lure2;
                break;
            case 3:
                lureData = oracle.level1Data.lure3;
                break;
            case 4:
                lureData = oracle.level1Data.lure4;
                break;
            case 5:
                lureData = oracle.level1Data.lure5;
                break;
            case 6:
                lureData = oracle.level1Data.lure6;
                break;
        }

        return lureData;
    }

    private void SetProgressBar(float currentCastTime)
    {
        castBar.value = castTime / currentCastTime;
    }

    private void Catch()
    {
        Roll((int)saveData.currentLure);
    }

    #region Rolling

    private void RollJunk()
    {
        var random = Random.Range(1, 6);
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
        var random = Random.Range(1, 12 - GetLureType((int)saveData.currentLure).junkChance / 5);
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
            case true:
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