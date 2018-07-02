using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {


    [SerializeField]
    Behaviour[] toDisable;

    private Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for ( int i = 0; i< toDisable.Length; i++)
            {
                toDisable[i].enabled = false;
            }
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

    void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }




}
