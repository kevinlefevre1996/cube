using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PlayerTag ="Player";
    

    [SerializeField]
    private PlayerWeapon weapon;

    [SerializeField]
    private GameObject weaponGFX;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private string weaponLayer = "weapon";

	// Use this for initialization
	void Start () {
		if(cam == null)
        {
            Debug.LogError("no cam ref");
            this.enabled = false;
        }

        weaponGFX.layer = 11;//LayerMask.NameToLayer(weaponLayer);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (PauseMenu.isOn)
        {
            return;
        }
	}




    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            // hit somthing
            //Debug.Log("hit: " + hit.collider.name);

            if (hit.collider.tag == PlayerTag)
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }

        }
    }

    [Command]
    void CmdPlayerShot(string playerid, int damage)
    {
        Debug.Log(playerid + " has been shot.");
        Player player= GameManager.GetPlayer(playerid);
        player.RpcTakeDamage(damage);


    }

}
