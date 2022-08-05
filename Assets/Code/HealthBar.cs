using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerController;
    public EnemyAI _enemy;
    private void Start()
    {
        if (_enemy != null)
        {
            SetMaxHealth(_enemy._maxHealth);
        }
        else if(playerController != null)
        {
            SetMaxHealth(playerController._maxHealth);
        }
    }

    private void Update()
    {
        if (_enemy != null)
        {
            SetHealth(_enemy._healthEnemy);
        }
        else if  (playerController != null)
        {
            SetHealth(playerController._health);
        }
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
