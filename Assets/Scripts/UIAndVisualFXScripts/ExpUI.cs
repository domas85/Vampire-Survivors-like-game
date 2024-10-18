using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;

    public Slider ExpBar;
    public float transitionSpeed = 3f;
    private float lerpSpeed;

    public void UpdateExpBar(int currentExp, int targetExp)
    {
        lerpSpeed = transitionSpeed * Time.deltaTime;
        // ExpBar.value = Mathf.Lerp(currentExp, ExpBar.value, lerpSpeed) ;
        ExpBar.value = currentExp;
        ExpBar.maxValue = targetExp;
    }

    public void SetLevelText(int level)
    {
        levelText.text = level.ToString();
    }
}
