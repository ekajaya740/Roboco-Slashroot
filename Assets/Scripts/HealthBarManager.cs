using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Color lowHealth;
    [SerializeField] private Color highHealth;
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update

    public void SetHealth(float healthNow, float maxHealth){
        healthBar.gameObject.SetActive(healthNow < maxHealth);
        healthBar.value = healthNow;
        healthBar.maxValue = maxHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHealth, highHealth, healthBar.normalizedValue);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
