using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value;  }
    }


    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;



    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i< disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        if( col != null)
        {
            col.enabled = true;
        }
    }
    [ClientRpc]
    public void RpcTakeDamage (int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        Debug.Log(transform.name + " Health:" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        Debug.Log(transform.name + " is dead");

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }


        //respawn

        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform spawnpoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnpoint.position;
        transform.rotation = spawnpoint.rotation;

        Debug.Log(transform.name + " Respawn");

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
		if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(99999);
        }
	}*/
}
