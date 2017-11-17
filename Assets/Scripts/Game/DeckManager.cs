using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour {

	public GameObject cardPrefabs;
	public int cardsCount=52;
	public Transform stack;

	public CardModel cardModel;

	List<GameObject> cards;
	// Use this for initialization
	void Awake(){
		cards = new List<GameObject> ();
		GenerateDeck ();
	}

	void onGUI(){
			
	}

	void Start () {
		Shuffle ();
	}

	void Shuffle(){
		int n = cards.Count;
		while (n > 1) {
			n--;
			int k = Random.Range (0, n + 1);
			cards [k].transform.SetParent (null);
			cards [n].transform.SetParent (null);	
			GameObject temp = cards [k];
			cards [k] = cards [n];
			cards [n] = temp;
			cards [k].transform.SetParent (stack);
			cards [n].transform.SetParent (stack);
		}
	}

	void GenerateDeck(){
		GameObject instantiatedCard = null;
		for (int card = 0; card < cardsCount; card++) {
			instantiatedCard = Instantiate (cardPrefabs, stack);
            //instantiatedCard.transform.position = Vector3.zero;
			cards.Add (instantiatedCard);
			SetCardSprites (instantiatedCard, card);
			SetCardFlower (instantiatedCard, card);
			instantiatedCard.GetComponent<Card> ().value = SetCardValue (card);
			SetCardObjectName (instantiatedCard);
		}
	}
	void SetCardObjectName(GameObject cardObject){
		Card card = cardObject.GetComponent<Card> ();
		if (card.value == 11)
			cardObject.name = "Jack " + card.flower.ToString ();
		else if (card.value == 12)
			cardObject.name = "Queen " + card.flower.ToString ();
		else if (card.value == 13)
			cardObject.name = "King " + card.flower.ToString ();
		else if (card.value == 14)
			cardObject.name = "As " + card.flower.ToString ();
		else
			cardObject.name = card.value.ToString () + " " + card.flower.ToString ();
	}

	int SetCardValue(int index){
		if (index < 13) {
			return index + 2;
		} else if (index < 13 * 2) {
			return index-13 + 2;
		} else if (index < 13 * 3) {
			return index-13*2 + 2;
		} else {
			return index-13*3 + 2;
		}
	}

	void SetCardSprites(GameObject cardObject, int modelIndex){
		Card currentCard = cardObject.GetComponent<Card> ();
		currentCard.cardFront = cardModel.cardFace [modelIndex];
		currentCard.cardBack = cardModel.cardBack;
		cardObject.GetComponent<Image> ().sprite = currentCard.cardFront;
		cardObject.transform.GetChild (0).GetComponent<Image> ().sprite = currentCard.cardBack;
	}

	void SetCardFlower(GameObject cardObject, int index){
		Card card = cardObject.GetComponent<Card> ();
		if (index < 13) {
			card.flowerIndex = 2;
			card.flower = CardModel.cardFlower.Hati;
		} else if (index < 13 * 2) {
			card.flowerIndex = 0;
			card.flower = CardModel.cardFlower.Wajik;
		} else if (index < 13 * 3) {
			card.flowerIndex = 1;
			card.flower = CardModel.cardFlower.Keriting;
		} else if (index < 13 * 4) {
			card.flowerIndex = 3;
			card.flower = CardModel.cardFlower.Sekop;
		}
	}
}
