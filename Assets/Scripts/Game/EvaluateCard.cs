using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EvaluateCard : MonoBehaviour {
	//public Transform handTransform;
	public List<Card> cards;
    int count1=0;
    int count2=0;
    int value1=0;
    int value2=0;

    public int handType = -1;
    public string hand="";
    public int highestValue = 0;
 

    string GetCardName(int index) {
        string cardname="";
        if (index <= 10)
            cardname = index.ToString();
        else if (index == 11)
            cardname = "Jack";
        else if (index == 12)
            cardname = "Queen";
        else if (index == 13)
            cardname = "King";
        else if (index == 14)
            cardname = "As";
        return cardname;
    }

    string GetSuitsName(int index) {
        string suits="";
        if (index == 0)
            suits = "Wajik";
        else if (index == 1)
            suits = "Keriting";
        else if (index == 2)
            suits = "Hati";
        else if (index == 3)
            suits = "Sekop";
        return suits;
    }

    int HighestValue() {
        int value = 0;
        foreach(Card c in cards) {
            if (c.value > value)
                value = c.value;
        }
        return value;
    }
    void Start(){
		cards = new List<Card> ();
        hand = "";
	}
	void Update(){
		AssessHand ();
	}

	void AssessHand(){
        if (cards.Count> 1) {
			
			var grouping = from card in cards
			               group card by card.value into g orderby g.Count() descending, g.Key
			               select g;
          
			count1 = grouping.FirstOrDefault ().Count ();
            value1 = grouping.FirstOrDefault().Key;
            
            if (cards.Count>=4)
            {
                count2 = grouping.ElementAt(1).Count();
                value2 = grouping.ElementAt(1).Key;
            }

        
            if (count1 == 4 && cards.Count>=4) {
				handType = 1;
				hand = "Empat " + GetCardName(value1);
                highestValue = value1;
			} else if (count1 == 3 && count2 >= 2 && cards.Count >= 5) {
				handType = 2;
				hand = "Full House - " + GetCardName(value1) + " dan " + GetCardName(value2);
                highestValue = value1;
            }
            else if (count1 == 3 && cards.Count>=3)
			{
				handType = 5;
				hand = "Tris " + GetCardName(value1);
                highestValue = value1;
            }
			else if (count1 == 2 && count2 == 2 && cards.Count>=4)
			{
				handType = 6;
				hand = "Dua Pair - " + GetCardName(value1) + " and " + GetCardName(value2);
                highestValue = value1;
            }
			else if (count1 == 2 && cards.Count >=2)
			{
				handType = 7;
				hand = "Pair " + GetCardName(value1);
                highestValue = value1;
            }
			else
			{
				handType = 8;
				hand = GetCardName(HighestValue()) + " high";
                highestValue = HighestValue();
            } 
            
			// sort by value
			cards = (from card in cards orderby card.value select card).ToList();

			Card[] distinctCards = cards.Distinct(new CardValueComparer()).ToArray();

			bool straight = false;

			if (distinctCards.Length >= 5)
			{ 
				for(int i = 0; i < distinctCards.Length - 4; i++)
				{
					if (distinctCards[i].value == distinctCards[i + 4].value - 4)
					{
						if (handType > 4)
						{
							hand = GetCardName(HighestValue())
                                /*GetCardName(distinctCards[i].value)*/
                                + "-high Straight";
							handType = 4;
                            highestValue = HighestValue();
                        } 
						straight = true;
						break;
					}
				}
			}
            
			grouping = from card in cards group card by card.flowerIndex into g 
				orderby g.Count() descending select g;

			

			int count = grouping.First().Count(); 

			if (count >= 5)
			{
				int index = grouping.First().First().value;
				int suit = grouping.First().Key;

				if (handType > 3)
				{
					handType = 3;
					hand = /*index*/  GetCardName(HighestValue()) + "-high Flush in " + GetSuitsName(suit);
                    highestValue = HighestValue();
                }

				if (straight)
				{        
					// now check for straight flush        
					Card[] flushCards = (from card in grouping.First() orderby card.value select card).ToArray(); 

					for(int i = 0; i < count - 4; i++)
					{
						if (flushCards[i].value == flushCards[i + 4].value - 4)
						{
							bool straightFlush = true;
							int flushSuit = flushCards[i].flowerIndex;

							for(int j = i + 1; j <  i + 5; j++)
							{
								if (flushSuit != flushCards[j].flowerIndex)
								{
									straightFlush = false;
									break; 
								}
							}

							if (straightFlush)
							{
								handType = 0;
								hand = /*GetCardName(flushCards[i].value)*/  GetCardName(HighestValue()) + "-high Straight Flush in " + GetSuitsName(flushCards[i].flowerIndex);
                                highestValue = HighestValue();
                                break;
							}
						}  
					}
				}
			}
		}
        //Debug.Log(transform.parent.name + " Hands value : " + hand);
    }
}
