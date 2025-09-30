using UnityEngine;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    public TMP_Text teamWinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameData.turnTeam1)
        {
            teamWinText.text = "Team 1";
            Camera.main.backgroundColor = Color.cornflowerBlue;
        }
        else
        {
            teamWinText.text = "Team 2";
            Camera.main.backgroundColor = Color.indianRed;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
