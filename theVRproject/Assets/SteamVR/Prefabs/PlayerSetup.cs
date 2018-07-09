using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {


    [SerializeField]
    Behaviour[] toDisable;

    [SerializeField]
    string remoteLayerName = "remotePlayer";

    [SerializeField]
    GameObject playerUI;

    private GameObject playerUIInstance;



    private Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }


            playerUIInstance = Instantiate(playerUI);
            playerUIInstance.name = playerUI.name;



        }

        GetComponent<Player>().Setup();



    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = 11; //LayerMask.NameToLayer(remoteLayerName); HARDCODED




    }




    void DisableComponents()
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        GameManager.UnregisterPlayer(transform.name);
    }




}
