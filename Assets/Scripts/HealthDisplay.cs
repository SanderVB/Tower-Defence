using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    int startHealth;
    Image healthBar;

    private void Start()
    {
        startHealth = FindObjectOfType<GameSession>().GetPlayerHealth();
        healthBar = GetComponent<Image>();
    }
    
    public void UpdateHealthDisplay(int currentHealth)
    {
        var healthBarScale = (float) currentHealth / startHealth;
        healthBar.transform.localScale = new Vector3(healthBarScale, 1, 1);
    }
}