using UnityEngine;

public class GameLifeManager : MonoBehaviour
{
    public int life = 3;          // 道中のライフ
    public int playerPoints = 0;  // プレイヤーのポイント

    // 道中でミスしたときに呼ぶ
    public void OnMistake()
    {
        life--;

        // ミスしたらポイントを減らす（例：-50）
        playerPoints = Mathf.Max(0, playerPoints - 50);

        Debug.Log("ミス！ ライフ：" + life + " / ポイント：" + playerPoints);

        // ライフが0でもゲームオーバーにはしない
        // 次の問題へ進む処理は別で呼ぶ
    }
}