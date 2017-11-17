using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //public vars
    public float bet=0f;
    public float money=10000f;
    public string playerName;
    public bool isHuman = false;

    //private vars
    bool dealt = false;
    bool drawed = false;
    int cardValue;
    string hand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetCardValue()
    {
        return cardValue;
    }

    public string GetHand()
    {
        return hand;
    }
}
