using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    public Button UpgradeButton;
    public TextMeshProUGUI UpgradeName;
    public TextMeshProUGUI UpgradeAmount;

    public UpgradeType UpgradeType; 

    private void OnEnable()
    {
        GenerateRandomUpgrade();
        UpgradeButton.onClick.AddListener(Upgrades);
    }

    private void GenerateRandomUpgrade()
    {
        UpgradeType = GetRandomUpgradeType();
        UpgradeName.text = UpgradeType.ToString();
        if(UpgradeType!=UpgradeType.OrbitAmount)
        UpgradeAmount.text ="+ "+ GetRandomAmount().ToString();
       else
            UpgradeAmount.text = "+ 1";

    }
    public void Upgrades()
    {
        UpgradeManager.Instance.IncreaseProperty(UpgradeType, GetRandomAmount());
    }
    private UpgradeType GetRandomUpgradeType() 
    {
        Array values = Enum.GetValues(typeof(UpgradeType));
        return (UpgradeType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    private int GetRandomAmount()
    {
        return UnityEngine.Random.Range(1, 4);
    }

    public void UpgradesProperty(UpgradeType upgradeType, Text upgradeAmount) 
    {
        UpgradeType = upgradeType;
        UpgradeName.text = UpgradeType.ToString();
        UpgradeAmount.text = upgradeAmount.text;
    }
}
