using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;


    void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage (int amount)
    {
        currentHealth -= amount;

        Debug.Log(transform.name + " Health:" + currentHealth);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
