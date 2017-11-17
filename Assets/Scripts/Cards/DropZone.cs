using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    List<Card> hand;

    void Start() {

    }

    void Update() {
        Debug.Log("Cards in " + name +" of "+transform.parent.name +" = " + transform.childCount);
    }

    public void OnDrop (PointerEventData eventData){
        hand = transform.parent.GetComponent<EvaluateCard>().cards;
        Draggable dragCard = eventData.pointerDrag.GetComponent<Draggable> ();
            if (dragCard != null)
            {
                dragCard.parentToReturn = transform;
                FlipCard(eventData.pointerDrag);
            }
            if (gameObject.name == "Hand") {
                Card thisCard = dragCard.GetComponent<Card>();
                //Check for duplicate
                if (!Duplicate(thisCard))
                    hand.Add(thisCard);
            }
            if (gameObject.name == "Table"){
                Card thisCard = dragCard.GetComponent<Card>();
                hand.Remove(thisCard);
            }
    }

    private bool Duplicate(Card card) {
        foreach (Card c in hand) {
            if (card == c)
                return true;
        }
        return false;
    }

    public void OnPointerEnter (PointerEventData eventData){
	
	}
	public void OnPointerExit (PointerEventData eventData){
	
	}
	public void FlipCard(GameObject card){
		card.transform.GetChild (0).gameObject.SetActive (false);
	}
}
