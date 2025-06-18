using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool StartPlay = false;

    // Changed from List to Queue to process pairs in order
    private Queue<Card> revealedCardsQueue = new Queue<Card>();

    public int totalPairs;
    public int matchedPairs;

    private bool isCheckingMatch = false;  // to avoid overlapping matches on same pair

    public void OnCardRevealed(Card card)
    {
        revealedCardsQueue.Enqueue(card);

        if (!isCheckingMatch && revealedCardsQueue.Count >= 2)
        {
            StartCoroutine(ProcessNextMatch());
        }
    }

    IEnumerator ProcessNextMatch()
    {
        isCheckingMatch = true;

        while (revealedCardsQueue.Count >= 2)
        {
            Card first = revealedCardsQueue.Dequeue();
            Card second = revealedCardsQueue.Dequeue();

            // Wait for a short pause (animation time)
            yield return new WaitForSeconds(0.2f);

            if (first.frontImage.sprite == second.frontImage.sprite)
            {
                first.matched();
                second.matched();
                matchedPairs++;
                Root.instance.uiManager.AddMatch();
                Root.instance.soundManager.PlayMatchSound();
                Debug.Log("Match! Total matched: " + matchedPairs + " / " + totalPairs);

                if (matchedPairs >= totalPairs)
                {
                    Debug.Log("YOU WIN!");
                    Root.instance.uiManager.WinPanel.ShowWinPanel();
                    Root.instance.levelManager.NextLevel();
                }
            }
            else
            {
                Root.instance.soundManager.PlayFailSound();
                // Flip back after a small delay for player to see
                yield return new WaitForSeconds(0.5f);
                first.SetHidden();
                second.SetHidden();
               
            }
        }

        isCheckingMatch = false;
    }

    public void UpdateCards(int _total, int _matched)
    {
        totalPairs = _total / 2;
        matchedPairs = _matched;
    }
}
