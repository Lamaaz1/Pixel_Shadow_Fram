using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Toggle[] levelToggles;
    private int currentLevelIndex;
    public GameObject cardPrefab;
    public Transform gridParent;
    public GridLayoutGroup gridLayoutGroup;
    public int count;
    public float StartShowTime=2f;
    public List<Sprite> availableImages; // your list of possible images
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    public void init()
    {
        for (int i = 0; i < levelToggles.Length; i++) 
        {
            levelToggles[i].GetComponent<DificultyButton>().LevelIndex = i;
        }
     
        LoadLevel();
    }
    public void LoadLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        // First reset all toggles
        for (int i = 0; i < levelToggles.Length; i++)
        {
            levelToggles[i].isOn = false;
        }

        // Set correct toggle ON
        levelToggles[currentLevel].isOn = true;
        //levelToggles[currentLevel].GetComponent<DificultyButton>().LoadL();


        // Force trigger event
        levelToggles[currentLevel].onValueChanged.Invoke(true);
    }
    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") < levelToggles.Length - 1)
        {
            // Increase level
            int newLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
            PlayerPrefs.SetInt("CurrentLevel", newLevel);
            PlayerPrefs.Save();

            // Reset all toggles
            for (int i = 0; i < levelToggles.Length; i++)
            {
                levelToggles[i].isOn = false;
            }

            // Activate new level toggle
            levelToggles[newLevel].isOn = true;
            levelToggles[newLevel].onValueChanged.Invoke(true);  // Force trigger if needed
        }
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
        Root.instance.gameManager.UpdateCards(count, 0);
        Root.instance.uiManager.ResetNumbers();
        AdjustGridLayout(count);
    }
 
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
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        // Step 1: Hide all cards
        foreach (Card card in gridParent.GetComponentsInChildren<Card>())
        {
            card.SetHidden();
        }

        // Step 2: Wait 1 second
        yield return new WaitForSeconds(0.3f);

        // Step 3: Shuffle list
        List<Card> cardList = new List<Card>(gridParent.GetComponentsInChildren<Card>());
        for (int i = 0; i < cardList.Count; i++)
        {
            Card temp = cardList[i];
            int randomIndex = Random.Range(i, cardList.Count);
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }

        // Step 4: Show cards one by one (random order)
        foreach (Card card in cardList)
        {
            card.SetRevealed();
            yield return new WaitForSeconds(0.05f);
        }

        // Step 5: Wait StartShowTime (player sees cards)
        yield return new WaitForSeconds(StartShowTime);

        // Step 6: Hide all cards again
        foreach (Card card in cardList)
        {
            card.SetHidden();
        }

        Root.instance.gameManager.StartPlay = true;
        Debug.Log("Player can start now!");
    }

}
