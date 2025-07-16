using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//マッチング画面で役職を決定する処理
public class DecideRole : MonoBehaviourPunCallbacks
{
    //ユーザーロール
    [SerializeField]string m_role = "ロール名";

    //プレイヤーの下に設置している役職名テキスト
    [SerializeField] Text[] m_playerRoleText = null;

    UserSettingData m_userSettingData = null;

    [SerializeField] RoleTable m_roleTable = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();
    }

    //ボタンを押されたときに選択している役職を変更する処理
    public void OnClickRoleButton()
    {
        m_userSettingData.GetSetPlayerRole = m_role;
        //テキストを更新
        photonView.RPC(nameof(UpdateRoleText), RpcTarget.All, m_userSettingData.GetSetPlayerId, m_userSettingData.GetSetPlayerRole);

        //デバック
        Debug.Log(m_userSettingData.GetSetPlayerRole + "に役職を変更しました。");

        //役職表を閉じる
        m_roleTable.OnClickRoleTableButton();
    }

    //役職テキスト更新処理
    [PunRPC]
    void UpdateRoleText(int playerId,string playerRole)
    {
        m_playerRoleText[playerId-1].text = playerRole;
    }
}