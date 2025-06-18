using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool StartPlay=false;
    public List<Card> revealedCards = new List<Card>();
    public int totalPairs;
    public int matchedPairs;

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
        yield return new WaitForSeconds(0.2f); // short pause

        if (revealedCards[0].frontImage.sprite == revealedCards[1].frontImage.sprite)
        {
            revealedCards[0].matched();
            revealedCards[1].matched();
            // It's a match!
            matchedPairs++;
            Root.instance.uiManager.AddMatch();

            Debug.Log("Match! Total matched: " + matchedPairs + " / " + totalPairs);

            // Check if player won
            if (matchedPairs >= totalPairs)
            {
                Debug.Log("YOU WIN!");
                Root.instance.uiManager.WinPanel.ShowWinPanel();
                Root.instance.levelManager.NextLevel();
                // show win panel, animation, sound, etc.
            }
        }
        else
        {
            // Not a match — flip both back
            revealedCards[0].SetHidden();
            revealedCards[1].SetHidden();
        }

        revealedCards.Clear();
    }
    public void UpdateCards(int _total,int _matched)
    {
        totalPairs = _total / 2;
        matchedPairs = _matched;
    }
}
