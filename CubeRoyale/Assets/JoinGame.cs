﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;

public class JoinGame : MonoBehaviour {

    private NetworkManager networkManager;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    // Use this for initialization
    void Start () {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }


        RefreshRoomList();

	}
	
    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (matchList == null)
        {
            status.text = "no list retreived";
            return;
        }

        ClearRoomList();

        foreach (MatchInfoSnapshot match in matchList)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if (_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }


            // as well as setting up a callback function that will join the game.

            roomList.Add(roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms at the moment.";
        }

    }


    void ClearRoomList()
    {
        for (int i = 0; i< roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        StartCoroutine(WaitForJoin());
    }

    IEnumerator WaitForJoin()
    {
        ClearRoomList();

        int countdown = 10;
        while (countdown > 0)
        {
            status.text = "JOINING... (" + countdown + ")";

            yield return new WaitForSeconds(1);

            countdown--;
        }

        // Failed to connect
        status.text = "Failed to connect.";
        yield return new WaitForSeconds(1);

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }

        RefreshRoomList();

    }
}
