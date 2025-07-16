using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;      //追加
using Photon.Realtime; //追加

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;
    UserSettingData m_userSettingData = null;
    //サバイバーのプレファブ
    [SerializeField] GameObject m_survivorPrefab;
    //ハンターのプレファブ
    [SerializeField] GameObject m_hunterPrefab;
    //プレイヤー名テキスト
    [SerializeField] Text[] m_playerNameText = null;
    //プレイヤーロールテキスト
    [SerializeField] Text[] m_playerRoleText = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();

        //プレイヤー名とロールを表示
        photonView.RPC(nameof(ActivePlayerNameAndRole), RpcTarget.All, m_userSettingData.GetSetPlayerId,PlayerPrefs.GetString("UserName", "NoName"));

        //サバイバーが選ばれていたら、
        if (m_userSettingData.GetSetIsSurvivorCamp)
        {
            m_gameObject = PhotonNetwork.Instantiate(
            m_survivorPrefab.name,
            new Vector3(0f, 1f, 0f),    //ポジション
            Quaternion.identity,        //回転
            0
            );
            //生成するゲームオブジェクトの名前をSurvivorにする
            m_gameObject.name = "Survivor";
        }
        //ハンターが選ばれていたら、
        else
        {
            m_gameObject = PhotonNetwork.Instantiate(
            m_hunterPrefab.name,
            new Vector3(0f, 1f, 0f),    //ポジション
            Quaternion.identity,        //回転
            0
            );
            //生成するゲームオブジェクトの名前をHunterにする
            m_gameObject.name = "Hunter";
        }

        //カメラをオンにする(使用する)
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("MiniMapCamera").GetComponent<Camera>().enabled = true;

        //デバック
        Debug.Log("ユーザー名は「" + PlayerPrefs.GetString("UserName", "NoName") + "」です");
        Debug.Log("役職は「" + m_userSettingData.GetSetPlayerRole + "」です");
    }

    [PunRPC]
    //プレイヤー名とロールを表示させる処理
    void ActivePlayerNameAndRole(int id,string name)
    {
        //プレイヤー名を表示
        m_playerNameText[id-1].text = name;
        //プレイヤーロールを表示
        m_playerRoleText[id-1].text = "「" + m_userSettingData.GetSetPlayerRole + "」";
    }
}