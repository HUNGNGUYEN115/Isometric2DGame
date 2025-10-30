using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
