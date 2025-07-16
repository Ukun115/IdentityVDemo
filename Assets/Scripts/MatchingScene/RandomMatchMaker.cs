using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;      //追加
using Photon.Realtime; //追加

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    UserSettingData m_userSettingData = null;

    [SerializeField]ReadyOkButton m_readyOkButton = null;

    [SerializeField] GameObject[] m_playerGameObject = null;

    [SerializeField] Text m_matchingNowText = null;

    bool m_delayTimerFlg = false;
    int m_delayTimer = 0;

    //プレイヤーID
    int m_playerId = 0;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();

        //サーバーに接続
        PhotonNetwork.ConnectUsingSettings();

        //デバック
        Debug.Log("サーバーに接続");
    }

    void Update()
    {
        //マッチング完了判定

        //部屋に入った人数が4人になったらマッチング完了
        //準備完了が全員になったら、
        if (PhotonNetwork.LocalPlayer.ActorNumber == 4 && !m_delayTimerFlg && m_readyOkButton.GetSetmReadyOkPlayerNumber == 4)
        {
            //デバック
            Debug.Log("ゲーム開始");

            //マッチング完了！にUI変更する
            m_matchingNowText.text = "マッチング完了！";

            //タイマー作動
            m_delayTimerFlg = true;
        }

        if (m_delayTimerFlg)
        {
            m_delayTimer++;
        }
        if (m_delayTimer > 120)
        {
            //ゲームシーンに遷移
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }
    }

    //サーバーへの接続が完了すると呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //ランダムで部屋に入室する
        PhotonNetwork.JoinRandomRoom();

        //デバック
        Debug.Log("サーバーへの接続が完了");
    }

    //ロビーへの入室が完了すると呼ばれるコールバック
    public override void OnJoinedLobby()
    {
        //ロビーに入室したら即座に部屋に入室する
        PhotonNetwork.JoinRandomRoom();

        //デバック
        Debug.Log("ロビーへの入室が完了");
    }

    // 入室に失敗した場合に呼ばれるコールバック
    // １人目は部屋がないため必ず失敗するので部屋を作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5; // 最大5人まで入室可能
        PhotonNetwork.CreateRoom(null, roomOptions); //第一引数はルーム名

        //デバック
        Debug.Log("入室に失敗(部屋を作成)");
    }

    //入室が完了したときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //今までこのルームに何人が入ってきたかでアクターナンバーが増えていく（アクターナンバーに書き込み不可）
        m_playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        //プレイヤーに5以上のIDが割り振られたことはどこかのタイミングで一人以上抜けている
        if (m_playerId >= 5)
        {
            //ルームにいる他のプレイヤーを取得
            Player[] otherPlayers = PhotonNetwork.PlayerListOthers;
            //他のプレイヤーに割り当てられている、使えない名前とIDを保存していく配列を定義
            var cantUseId = new List<string>();

            foreach (var pl in otherPlayers)
            {
                //既に使っているIDを保存していく
                cantUseId.Add(pl.NickName);
            }

            //Player1という名前のユーザーがいなければ、ID1を使用する。
            if (!cantUseId.Contains("Player1"))
            {
                m_playerId = 1;
            }
            else if (!cantUseId.Contains("Player2"))
            {
                m_playerId = 2;
            }
            else if (!cantUseId.Contains("Player3"))
            {
                m_playerId = 3;
            }
            else if (!cantUseId.Contains("Player4"))
            {
                m_playerId = 4;
            }
        }
        //IDを登録
        m_userSettingData.GetSetPlayerId = m_playerId;
        //プレイヤーを表示
        photonView.RPC(nameof(OnOffPlayerModel), RpcTarget.All);

        //デバック
        Debug.Log("入室が完了");
    }

    //プレイヤーを表示する関数
    [PunRPC]
    void OnOffPlayerModel()
    {
        //参加しているプレイヤー分表示
        foreach (var i in PhotonNetwork.PlayerList)
        {
            //デバック
            Debug.Log("プレイヤー" + i.ActorNumber + "が参加中");

            //参加しているプレイヤ―のモデルを表示
            m_playerGameObject[i.ActorNumber-1].SetActive(true);

            //参加しているプレイヤーのテキストを表示
            GameObject.Find("PlayerName" + i.ActorNumber + "Text").GetComponent<Text>().enabled = true;
            //参加しているプレイヤーのテキストをユーザー名にする
            GameObject.Find("PlayerName" + i.ActorNumber + "Text").GetComponent<Text>().text = PlayerPrefs.GetString("UserName", "NoName");
            //参加しているプレイヤーの役職テキストを表示
            GameObject.Find("Player" + i.ActorNumber + "RoleText").GetComponent<Text>().enabled = true;
        }
    }

    //ゲームシーンに移行する関数
    [PunRPC]
    void GoGameScene()
    {
        //ゲームシーンに移行
        SceneManager.LoadScene("GameScene");
    }
}