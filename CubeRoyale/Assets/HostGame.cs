using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomsize = 16;

    
    private string roomName;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }


    public void SetRoomName (string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        if( roomName != "" && roomName != null)
        {
            Debug.Log("Creating Room: "+roomName + " with room for " + roomsize + " players");
            //Create room
            networkManager.matchMaker.CreateMatch(roomName, roomsize, true, "","" ,"", 0 ,0, networkManager.OnMatchCreate);
        }
    }
}
