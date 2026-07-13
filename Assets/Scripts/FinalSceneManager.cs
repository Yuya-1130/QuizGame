using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public FinalQuestion question;
    public int life = 3;
    public int playerPoints;
    public int revealCount;

    private string[] shuffledChoices;
    private int correctIndexAfterShuffle;

    void Start()
    {
        playerPoints = PlayerPrefs.GetInt("PlayerPoints");
        revealCount = PlayerPrefs.GetInt("RevealCount");

        ApplyRandomMask();
        ShuffleChoices();
    }

    void ApplyRandomMask()
    {
        foreach (var c in question.chars)
            c.isRevealed = false;

        for (int i = 0; i < revealCount; i++)
        {
            int r = Random.Range(0, question.chars.Length);
            question.chars[r].isRevealed = true;
        }
    }

    void ShuffleChoices()
    {
        shuffledChoices = (string[])question.choices.Clone();

        for (int i = shuffledChoices.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            string temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[r];
            shuffledChoices[r] = temp;
        }

        for (int i = 0; i < shuffledChoices.Length; i++)
        {
            if (shuffledChoices[i] == question.choices[question.correctIndex])
            {
                correctIndexAfterShuffle = i;
                break;
            }
        }
    }

    public void OnChoiceSelected(int index)
    {
        if (index == correctIndexAfterShuffle)
        {
            Debug.Log("正解！");
        }
        else
        {
            life--;
            Debug.Log("不正解！ ライフ：" + life);

            if (life <= 0)
            {
                SceneManager.LoadScene("ScoreScene");
            }
        }
    }

    public string[] GetChoices()
    {
        return shuffledChoices;
    }
}