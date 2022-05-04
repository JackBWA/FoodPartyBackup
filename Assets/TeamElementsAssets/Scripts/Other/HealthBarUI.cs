using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    private BoardEntity entity;

    public Image playerAvatar;
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI coinsDisplay;
    public TextMeshProUGUI healthTextDisplay;
    public Slider healthGfxDisplay;

    public void Initialize(BoardEntity entity)
    {
        this.entity = entity;
        nameDisplay.text = entity.GetComponent<PlayerCharacter>().name;
        healthTextDisplay.text = $"{entity.health}/{entity.baseHealth}";
        healthGfxDisplay.value = entity.health / entity.baseHealth;
        coinsDisplay.text = $"{entity.coins}";
        enabled = true; // Weird but works.
    }

    private void OnEnable()
    {
        if (entity != null)
        {
            entity.onHealthChange += UpdateHealthBar;
            entity.onCoinsChange += UpdateCoins;
        }
    }

    private void OnDisable()
    {
        if (entity != null)
        {
            entity.onHealthChange -= UpdateHealthBar;
            entity.onCoinsChange -= UpdateCoins;
        }
    }

    public void UpdateHealthBar(float value)
    {
        //Debug.Log("Tira pero este si");
        healthTextDisplay.text = $"{value}/{entity.baseHealth}";
        healthGfxDisplay.value = value / entity.baseHealth;
    }

    public void UpdateCoins(int coins)
    {
        //Debug.Log("Tira");
        coinsDisplay.text = $"{coins}";
    }
}
