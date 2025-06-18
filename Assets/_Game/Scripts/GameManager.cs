using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool StartPlay;
    public List<Card> revealedCards = new List<Card>();

    public void OnCardRevealed(Card card)
    {
        revealedCards.Add(card);

        if (revealedCards.Count == 2)
        {
            // Check match!
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f); // short pause

        if (revealedCards[0].frontImage.sprite == revealedCards[1].frontImage.sprite)
        {
            // It's a match — keep revealed!
            Debug.Log("Match!");
        }
        else
        {
            // Not a match — flip both back
            revealedCards[0].SetHidden();
            revealedCards[1].SetHidden();
        }

        revealedCards.Clear();
    }
}
