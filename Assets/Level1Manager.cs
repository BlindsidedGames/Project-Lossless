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
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private Animator catchAnimator;
    [SerializeField] private TMP_Text catchText;

    [SerializeField] private TMP_Text tuna;
    [SerializeField] private TMP_Text salmon;
    [SerializeField] private TMP_Text herring;
    [SerializeField] private TMP_Text sardines;
    [SerializeField] private TMP_Text trout;
    [SerializeField] private TMP_Text halibut;
    [SerializeField] private TMP_Text flounder;
    [SerializeField] private TMP_Text cod;
    [SerializeField] private TMP_Text anchovies;
    [SerializeField] private TMP_Text shrimp;

    [SerializeField] private TMP_Text boot;
    [SerializeField] private TMP_Text can;
    [SerializeField] private TMP_Text tyre;
    [SerializeField] private TMP_Text plasticBottle;
    [SerializeField] private TMP_Text plant;

    private float castTime;
    private static readonly int Catch1 = Animator.StringToHash("Catch");

    private void Start()
    {
        SetImageAndCost();
        lurePurchaseButton.onClick.AddListener(PurchaseLure);
        if ((int)saveData.currentLure + 1 == 7) saveData.complete = true;
    }

    private void SetImageAndCost()
    {
        lurePurchaseImage.sprite = (int)saveData.currentLure + 1 < 7
            ? GetLureType((int)saveData.currentLure + 1).lureImage
            : GetLureType((int)saveData.currentLure).lureImage;
        lurePurchaseCost.text =
            $"{((int)saveData.currentLure + 1 < 7 ? "$" + CalcUtils.FormatNumber(GetLureType((int)saveData.currentLure + 1).lureCost) : "Max")}";
        castBar.image.sprite = GetLureType((int)saveData.currentLure).lureImage;
    }

    private void PurchaseLure()
    {
        SetImageAndCost();
        if (saveData.cash < GetLureType((int)saveData.currentLure + 1).lureCost) return;
        if ((int)saveData.currentLure + 1 == 7) saveData.complete = true;
        saveData.currentLure += 1;
        saveData.cash -= GetLureType((int)saveData.currentLure).lureCost;
        SetImageAndCost();
    }

    private void Update()
    {
        lurePurchaseButton.interactable = saveData.cash > GetLureType((int)saveData.currentLure + 1).lureCost &&
                                          (int)saveData.currentLure + 1 < 7;
        CastBar();
        UpdateTexts();
        saveData.timeSpentInLevel += Time.deltaTime;
    }

    private void UpdateTexts()
    {
        cashText.text = $"${CalcUtils.FormatNumber(saveData.cash)}";

        tuna.text = $"{saveData.tuna:N0}";
        salmon.text = $"{saveData.salmon:N0}";
        herring.text = $"{saveData.herring:N0}";
        sardines.text = $"{saveData.sardines:N0}";
        trout.text = $"{saveData.trout:N0}";
        halibut.text = $"{saveData.halibut:N0}";
        flounder.text = $"{saveData.flounder:N0}";
        cod.text = $"{saveData.cod:N0}";
        anchovies.text = $"{saveData.anchovies:N0}";
        shrimp.text = $"{saveData.shrimp:N0}";

        boot.text = $"{saveData.boot:N0}";
        can.text = $"{saveData.can:N0}";
        tyre.text = $"{saveData.tyre:N0}";
        plasticBottle.text = $"{saveData.plasticBottle:N0}";
        plant.text = $"{saveData.plant:N0}";
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
                catchText.text = "+$0.00 <size=150%><sprite=0>";
                break;
            case 2:
                saveData.can++;
                catchText.text = "+$0.00 <size=150%><sprite=1>";
                break;
            case 3:
                saveData.tyre++;
                catchText.text = "+$0.00 <size=150%><sprite=2>";
                break;
            case 4:
                saveData.plasticBottle++;
                catchText.text = "+$0.00 <size=150%><sprite=3>";
                break;
            case 5:
                saveData.plant++;
                catchText.text = "+$0.00 <size=150%><sprite=4>";
                break;
        }

        catchAnimator.SetTrigger(Catch1);
    }

    private void RollFish()
    {
        var random = Random.Range(1, 12 - GetLureType((int)saveData.currentLure).junkChance / 5);
        switch (random)
        {
            case 1:
                saveData.shrimp++;
                catchText.text = "+$10 <size=150%><sprite=14>";
                saveData.cash += 10;
                break;
            case 2:
                saveData.anchovies++;
                catchText.text = "+$25 <size=150%><sprite=13>";
                saveData.cash += 25;
                break;
            case 3:
                saveData.cod++;
                catchText.text = "+$50 <size=150%><sprite=12>";
                saveData.cash += 50;
                break;
            case 4:
                saveData.flounder++;
                catchText.text = "+$80 <size=150%><sprite=11>";
                saveData.cash += 80;
                break;
            case 5:
                saveData.halibut++;
                catchText.text = "+$100 <size=150%><sprite=10>";
                saveData.cash += 100;
                break;
            case 6:
                saveData.trout++;
                catchText.text = "+$300 <size=150%><sprite=9>";
                saveData.cash += 300;
                break;
            case 7:
                saveData.sardines++;
                catchText.text = "+$400 <size=150%><sprite=8>";
                saveData.cash += 400;
                break;
            case 8:
                saveData.herring++;
                catchText.text = "+$700 <size=150%><sprite=7>";
                saveData.cash += 700;
                break;
            case 9:
                saveData.salmon++;
                catchText.text = "+$800 <size=150%><sprite=6>";
                saveData.cash += 800;
                break;
            case 10:
                saveData.tuna++;
                catchText.text = "+$1K <size=150%><sprite=5>";
                saveData.cash += 1000;
                break;
            default:
                Debug.Log("InvalidRoll");
                break;
        }

        catchAnimator.SetTrigger(Catch1);
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