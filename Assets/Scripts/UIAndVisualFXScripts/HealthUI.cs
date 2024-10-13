using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public Slider healthBar;
    public Player playerObject;
    public float transitionSpeed = 3f;
    private float lerpSpeed;
    private float refValue;
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerObject.playerData.maxHealth;
        healthBar.value = playerObject.playerData.Health;
    }
    public void SetHealth(float hp)
    {
        
        lerpSpeed = transitionSpeed * Time.deltaTime;
        healthBar.value = Mathf.Lerp(healthBar.value, hp, lerpSpeed);
        //healthBar.value = Mathf.SmoothDamp(healthBar.value, hp,  ref refValue, transitionSpeed);

        //healthBar.value = hp;
    }
}

