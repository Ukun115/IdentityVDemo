using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 準備完了、準備中、ボタン分岐処理
/// </summary>
public class ReadyOkButton : MonoBehaviourPunCallbacks
{
    //ボタン状態
    bool m_isReadyOk = false;
    //準備完了ボタンのテキスト
    [SerializeField]Text m_readyText = null;
    //準備完了人数
    int m_readyOkPlayer = 0;
    //準備完了人数テキスト
    [SerializeField] Text m_waitPlayerText = null;
    //チェックマーク画像
    [SerializeField] Image[] m_checkMarkImage = null;
    //
    UserSettingData m_userSettingData = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();
    }

    //ボタン押されたらボタン状態を切替え
    public void OnClickReadyButton()
    {
        //判定を逆にする
        m_isReadyOk = !m_isReadyOk;

        //準備完了状態のとき、
        if(m_isReadyOk)
        {
            m_readyText.text = "準備キャンセル";

            //準備完了人数を+1する
            photonView.RPC(nameof(AddReadyOkPlayer), RpcTarget.All, m_userSettingData.GetSetPlayerId-1);
        }
        //準備中の時、
        else
        {
            m_readyText.text = "準備完了";

            //準備完了人数を-1する
            photonView.RPC(nameof(ReduceReadyOkPlayer), RpcTarget.All, m_userSettingData.GetSetPlayerId-1);
        }
    }

    //準備完了人数のプロパティ
    public int GetSetmReadyOkPlayerNumber
    {
        get {return m_readyOkPlayer; }
        set { m_readyOkPlayer = value; }
    }

    [PunRPC]
    void AddReadyOkPlayer(int id)
    {
        m_readyOkPlayer++;
        //準備完了人数テキストを更新
        m_waitPlayerText.text = m_readyOkPlayer + "/5";
        //チェックマークをオン
        m_checkMarkImage[id].enabled = true;
    }
    [PunRPC]
    void ReduceReadyOkPlayer(int id)
    {
        m_readyOkPlayer--;
        //準備完了人数テキストを更新
        m_waitPlayerText.text = m_readyOkPlayer + "/5";
        //チェックマークをオフ
        m_checkMarkImage[id].enabled = false;
    }
}