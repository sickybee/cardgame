using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealCard : MonoBehaviour {
    public Transform stack;
    public Transform handP1;
    public Transform handP2;
    public Button dealP1;
    public Button dealP2;
    
    public int cardsPerHand=13;

    CardModel cardModel;
	// Use this for initialization
	void Start () {
        dealP1.GetComponent<Button>().onClick.AddListener(dealToP1);
        dealP2.GetComponent<Button>().onClick.AddListener(dealToP2);
    }
	
	// Update is called once per frame
	void Update () {
   
	}

    /*void OnGUI()
    {
        dealTrigger = GUI.Button(new Rect(25, 25, 100, 30), "Deal");
    }*/
    void dealToP1() {
        deal(handP1);
    }
    void dealToP2() {
        deal(handP2);
    }

    void deal(Transform hand) {
        if (stack.childCount >=cardsPerHand)
        {
            for (int i = 0; i < cardsPerHand; i++)
            {
                Card currCard = stack.GetChild(i).GetComponent<Card>();
                currCard.transform.SetParent(hand);
                currCard.transform.GetChild(0).gameObject.SetActive(false);
            }
            Debug.Log("Cards in stack = " + stack.childCount);
        }
    }

    void flipCard(Card card) {
        card.GetComponent<Image>().sprite = card.cardFront;
    }
}
