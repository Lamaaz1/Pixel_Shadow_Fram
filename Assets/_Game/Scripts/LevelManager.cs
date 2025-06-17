using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;
    public GridLayoutGroup gridLayoutGroup;
    public int count;

    public List<Sprite> availableImages; // your list of possible images
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountModify(int c)
    {
        count = c;
    }
    public void CreateCards()
    {
        // Clear existing cards
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        int pairCount = count / 2;
        List<Sprite> cardImages = new List<Sprite>();

        // Repeat photos if needed
        for (int i = 0; i < pairCount; i++)
        {
            int imageIndex = i % availableImages.Count;
            Sprite image = availableImages[imageIndex];

            cardImages.Add(image); // first copy
            cardImages.Add(image); // second copy
        }

        // Shuffle
        for (int i = 0; i < cardImages.Count; i++)
        {
            Sprite temp = cardImages[i];
            int randomIndex = Random.Range(i, cardImages.Count);
            cardImages[i] = cardImages[randomIndex];
            cardImages[randomIndex] = temp;
        }

        // Instantiate cards
        for (int i = 0; i < cardImages.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            card.GetComponent<Card>().SetImage(cardImages[i]);
        }

        AdjustGridLayout(count);
    }
    //void AdjustGridLayout(int totalCards)
    //{
    //    int rows = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
    //    int columns = Mathf.CeilToInt((float)totalCards / rows);

    //    // Get panel size
    //    RectTransform rt = gridParent.GetComponent<RectTransform>();
    //    float panelWidth = rt.rect.width;
    //    float panelHeight = rt.rect.height;

    //    float spacing = gridLayoutGroup.spacing.x;
    //    float cellWidth = (panelWidth - ((columns - 1) * spacing)) / columns;
    //    float cellHeight = (panelHeight - ((rows - 1) * spacing)) / rows;

    //    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    //    gridLayoutGroup.constraintCount = columns;
    //    gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
    //}
    void AdjustGridLayout(int totalCards)
    {
        List<(int columns, int rows)> possibleGrids = new List<(int columns, int rows)>();

        // Find all clean grids (columns >= rows), and no 1 row / 1 column
        for (int i = 2; i <= totalCards; i++)
        {
            if (totalCards % i == 0)
            {
                int columns = i;
                int rows = totalCards / i;

                if (rows >= 2 && columns >= rows)
                {
                    possibleGrids.Add((columns, rows));
                }
            }
        }

        if (possibleGrids.Count == 0)
        {
            Debug.LogWarning($"No valid grid found for {totalCards} cards!");
            return;
        }

        // Pick the "smallest column count" that satisfies columns >= rows
        var bestGrid = possibleGrids[0]; // Default: first one
        foreach (var grid in possibleGrids)
        {
            if (grid.columns < bestGrid.columns)
            {
                bestGrid = grid;
            }
        }

        int bestColumns = bestGrid.columns;
        int bestRows = bestGrid.rows;

        // Get panel size
        RectTransform rt = gridParent.GetComponent<RectTransform>();
        float panelWidth = rt.rect.width;
        float panelHeight = rt.rect.height;

        float spacingX = gridLayoutGroup.spacing.x;
        float spacingY = gridLayoutGroup.spacing.y;

        // Calculate square size
        float cellWidth = (panelWidth - spacingX * (bestColumns - 1)) / bestColumns;
        float cellHeight = (panelHeight - spacingY * (bestRows - 1)) / bestRows;

        float cellSize = Mathf.Min(cellWidth, cellHeight); // Make it square

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = bestColumns;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        Debug.Log($"Grid picked: {bestColumns} columns x {bestRows} rows");
    }

}
