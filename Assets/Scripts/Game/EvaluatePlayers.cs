using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluatePlayers : MonoBehaviour {
    public Transform player1;
    public Transform player2;
    public Text winningText;
    public static string winner="";
    public static string winningHand ="";

    // Use this for initialization
    void Start () {
        winningText.text = "";
    }

    void evaluatePlayers(Transform p1, Transform p2) {
        int p1Hand = p1.GetComponent<EvaluateCard>().handType;
        int p2Hand = p2.GetComponent<EvaluateCard>().handType;

        if (p1Hand < p2Hand)
        {
            winner = p1.name;
            winningHand = p1.GetComponent<EvaluateCard>().hand;
        }
        else if (p1Hand == p2Hand)
        {
            int p1CardValue = p1.GetComponent<EvaluateCard>().highestValue;
            int p2CardValue = p2.GetComponent<EvaluateCard>().highestValue;
            if (p1CardValue > p2CardValue)
            {
                winner = p1.name;
                winningHand = p1.GetComponent<EvaluateCard>().hand;
            }
            else
            {
                winner = p2.name;
                winningHand = p2.GetComponent<EvaluateCard>().hand;
            }

        }
        else
        {
            winner = p2.name;
            winningHand = p2.GetComponent<EvaluateCard>().hand;
        }
        winningText.text = "Winner : " + winner + " with " + winningHand;
    }

	// Update is called once per frame
	void Update () {
        if(player1.Find("Hand").childCount>1 && player2.Find("Hand").childCount>1)
            evaluatePlayers(player1, player2);
        else
            winningText.text = "";
    }
}