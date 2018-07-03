using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {


    [SerializeField]
    Behaviour[] toDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

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
            
        }

        



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
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);




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
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        GameManager.UnregisterPlayer(transform.name);
    }




}
