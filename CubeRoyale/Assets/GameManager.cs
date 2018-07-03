﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private const string PLAYERID = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();


    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = PLAYERID + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer (string playerID)
    {
        return players[playerID];
    }

    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string playerID in players.Keys)
        {
            GUILayout.Label(playerID + "  -  " + players[playerID].transform.name);
        }



        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/





    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}