using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValueComparer : IEqualityComparer<Card> {
	public bool Equals(Card c1, Card c2)
	{
		return c1.value == c2.value;
	}

	public int GetHashCode(Card c)
	{
		return c.value.GetHashCode();
	}
}
