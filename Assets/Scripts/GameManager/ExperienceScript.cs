using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceScript : MonoBehaviour
{
    public static ExperienceScript Instance;
    public int TotalExp;
    public Slider ExpSlider;
    public int ExpIncreaseAmount = 1;
    public GameObject LevelIncreaseText;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        ExpSlider.minValue = 0;
        ExpSlider.maxValue = 100;
    }
    public void EarnExp(int amount)
    {
        TotalExp += (amount*ExpIncreaseAmount);
        if (TotalExp >= 100)
        {
            PlayerController.Instance.UpdateScaleText();
            TotalExp = 0;
            StartCoroutine(IlevelText());
            
        }
        ExpSlider.value = TotalExp;
    }
    IEnumerator IlevelText()
    {
        LevelIncreaseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        LevelIncreaseText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        LevelIncreaseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LevelIncreaseText.gameObject.SetActive(false);
    }
}
