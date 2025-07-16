using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残り解読数
/// </summary>
public class ChangeLimitDecodingNumber : MonoBehaviour
{
    //残り解読数
    int m_limitDecodingNumber = 5;

    //残り解読数を減らす処理
    public void ReduceLimitDecodingNumber()
    {
        //残り解読数を減らす
        m_limitDecodingNumber--;

        //テキストコンポーネントを取得し、
        //残り解読数を更新する
        if (m_limitDecodingNumber != 0)
        {
            this.GetComponent<Text>().text = m_limitDecodingNumber + "個の暗号がまだ解読されていません";

            //暗号機の解読が２台終わったら、
            if(m_limitDecodingNumber == 3)
            {
                //ハッチのテキスト
                GameObject.Find("HatchText").GetComponent<Text>().text = "地下室が更新済みです";

                //ハッチを使用可能にする
                GameObject.Find("HitArea").GetComponent<GoResult>().SetCanGoResult(true);
            }
        }
        //暗号機5つを解読完了したとき、
        else
        {
            this.GetComponent<Text>().text = "サバイバーは電源スイッチを入れることができます";
        }
    }

    //残り解読数を取得するゲッター
    public int GetLimitDecodingNumber()
    {
        return m_limitDecodingNumber;
    }
}