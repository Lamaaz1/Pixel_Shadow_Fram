using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;
    public GridLayoutGroup gridLayoutGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateCards(int count)
    {
        // Clear existing cards
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // Create pairs (count must be even)
        int pairCount = count / 2;
        List<int> cardIDs = new List<int>();

        for (int i = 0; i < pairCount; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        // Shuffle
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int temp = cardIDs[i];
            int randomIndex = Random.Range(i, cardIDs.Count);
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        // Instantiate
        for (int i = 0; i < cardIDs.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            //card.GetComponent<Card>().SetID(cardIDs[i]); // You can define how your Card script handles this
        }

        AdjustGridLayout(count);
    }
    void AdjustGridLayout(int totalCards)
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
        int columns = Mathf.CeilToInt((float)totalCards / rows);

        // Get panel size
        RectTransform rt = gridParent.GetComponent<RectTransform>();
        float panelWidth = rt.rect.width;
        float panelHeight = rt.rect.height;

        float spacing = gridLayoutGroup.spacing.x;
        float cellWidth = (panelWidth - ((columns - 1) * spacing)) / columns;
        float cellHeight = (panelHeight - ((rows - 1) * spacing)) / rows;

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
