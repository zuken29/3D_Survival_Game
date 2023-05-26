using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : Singleton<PlayerState>
{
    // ---- Player Health ---- // 
    public float currentHealth;
    public float maxHealth;
    
    
    // ---- Player Calories ---- // 
    public float currentCalories;
    public float maxCalories;

    public float distanceTravelled = 0;
    private Vector3 lastPosition;

    public GameObject playerBody;
    
    // ---- Player Hydration ---- //
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    public bool isHydrationActive;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {
            currentHydrationPercent -= 1;
            yield return new WaitForSeconds(10);
        }
    }


    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentCalories -= 1;
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentHealth -= 10;
        }
    }

    public void setHealth(float newHealth)      
    {
        currentHealth = newHealth;
    }

    public void setCalories(float newCalories)
    {
        currentCalories = newCalories;
    }

    public void setHydration(float newHydration)
    {
        currentHydrationPercent = newHydration;
    }
}
