using UnityEngine;

public class ScoreSceneManager : MonoBehaviour
{
    void Start()
    {
        int score = PlayerPrefs.GetInt("PlayerPoints");

        Debug.Log("最終スコア：" + score);
        // UIで表示する
    }
}