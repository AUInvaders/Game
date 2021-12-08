using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerHealthManager : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public static PlayerHealthManager Instance;

    int Health = PlayerShip.Health;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        HealthText.text = Health.ToString() + " Health";
    }

    // Update is called once per frame
    public void UpdateHealth(int hp)
    {
        Health -= hp;
        if (Health < 0) 
        { 
            Health = 0;
            HealthText.text = Health.ToString() + " Health";
        }
        else
        {
            HealthText.text = Health.ToString() + " Health";
        }
    }
}
