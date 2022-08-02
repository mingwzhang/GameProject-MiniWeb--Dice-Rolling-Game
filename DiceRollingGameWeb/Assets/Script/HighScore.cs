using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighScore : MonoBehaviour
{

    private class HighScoreEntry
    {
        public int score;
        public string name;
    }


    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("Score Container");
        entryTemplate = entryContainer.Find("High Scores Template");

        entryTemplate.gameObject.SetActive(false);

        highScoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{ score = 1234, name = "MZ"},
        };

        // Sort entry list by Score
        for (int i = 0; i < highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScoreEntryList.Count; j++)
            {
                if (highScoreEntryList[j].score > highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry temp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = temp;
                }
            }
        }


        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in highScoreEntryList)
        {
            CreateHighStoreEntryTransform(highscoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

 
    private void CreateHighStoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        entryTransform.Find("PL pos").GetComponent<Text>().text = rankString;
        int score = highScoreEntry.score;

        entryTransform.Find("Score pos").GetComponent<Text>().text = score.ToString();
        string name = highScoreEntry.name;

        entryTransform.Find("Name pos").GetComponent<Text>().text = name.ToString();

        transformList.Add(entryTransform);
    }
}
