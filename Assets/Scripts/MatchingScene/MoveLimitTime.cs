using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// マッチング画面の残り時間を進める処理
/// </summary>
public class MoveLimitTime : MonoBehaviourPunCallbacks
{
    //秒
    float m_seconds = 60.5f;
    //分
    int m_minutes = 2;
    //制限時間テキスト
    [SerializeField] Text m_limitTimeText = null;

    void Update()
    {
        //ホストのみ実行
        if (PhotonNetwork.IsMasterClient)
        {
            //時間をカウントダウンする
            m_seconds -= Time.deltaTime;

            //秒が0になったら、
            if(m_seconds <= 0.0f)
            {
                //秒を初期化
                m_seconds = 60.0f;
                //分を-1する
                m_minutes--;
                //デバック
                Debug.Log("１分経過");
            }

            //時間を全プレイヤーに表示する
            photonView.RPC(nameof(UpdateLimitTimeText), RpcTarget.All, m_minutes, m_seconds);
        }
    }

    //時間を全プレイヤーに表示する
    [PunRPC]
    void UpdateLimitTimeText(int minute,float second)
    {
        //時間を表示する

        //秒が１桁の場合
        if (second < 10.0f)
        {
            m_limitTimeText.text = "0" + minute + ":0" + second.ToString("f0");
        }
        //２桁の場合
        else
        {
            m_limitTimeText.text = "0" + minute + ":" + second.ToString("f0");
        }

        return;
    }
}