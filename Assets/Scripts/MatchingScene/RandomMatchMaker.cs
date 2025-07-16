using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;      //�ǉ�
using Photon.Realtime; //�ǉ�

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    UserSettingData m_userSettingData = null;

    [SerializeField]ReadyOkButton m_readyOkButton = null;

    [SerializeField] GameObject[] m_playerGameObject = null;

    [SerializeField] Text m_matchingNowText = null;

    bool m_delayTimerFlg = false;
    int m_delayTimer = 0;

    //�v���C���[ID
    int m_playerId = 0;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();

        //�T�[�o�[�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();

        //�f�o�b�N
        Debug.Log("�T�[�o�[�ɐڑ�");
    }

    void Update()
    {
        //�}�b�`���O��������

        //�����ɓ������l����4�l�ɂȂ�����}�b�`���O����
        //�����������S���ɂȂ�����A
        if (PhotonNetwork.LocalPlayer.ActorNumber == 4 && !m_delayTimerFlg && m_readyOkButton.GetSetmReadyOkPlayerNumber == 4)
        {
            //�f�o�b�N
            Debug.Log("�Q�[���J�n");

            //�}�b�`���O�����I��UI�ύX����
            m_matchingNowText.text = "�}�b�`���O�����I";

            //�^�C�}�[�쓮
            m_delayTimerFlg = true;
        }

        if (m_delayTimerFlg)
        {
            m_delayTimer++;
        }
        if (m_delayTimer > 120)
        {
            //�Q�[���V�[���ɑJ��
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }
    }

    //�T�[�o�[�ւ̐ڑ�����������ƌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //�����_���ŕ����ɓ�������
        PhotonNetwork.JoinRandomRoom();

        //�f�o�b�N
        Debug.Log("�T�[�o�[�ւ̐ڑ�������");
    }

    //���r�[�ւ̓�������������ƌĂ΂��R�[���o�b�N
    public override void OnJoinedLobby()
    {
        //���r�[�ɓ��������瑦���ɕ����ɓ�������
        PhotonNetwork.JoinRandomRoom();

        //�f�o�b�N
        Debug.Log("���r�[�ւ̓���������");
    }

    // �����Ɏ��s�����ꍇ�ɌĂ΂��R�[���o�b�N
    // �P�l�ڂ͕������Ȃ����ߕK�����s����̂ŕ������쐬����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5; // �ő�5�l�܂œ����\
        PhotonNetwork.CreateRoom(null, roomOptions); //�������̓��[����

        //�f�o�b�N
        Debug.Log("�����Ɏ��s(�������쐬)");
    }

    //���������������Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        //���܂ł��̃��[���ɉ��l�������Ă������ŃA�N�^�[�i���o�[�������Ă����i�A�N�^�[�i���o�[�ɏ������ݕs�j
        m_playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        //�v���C���[��5�ȏ��ID������U��ꂽ���Ƃ͂ǂ����̃^�C�~���O�ň�l�ȏ㔲���Ă���
        if (m_playerId >= 5)
        {
            //���[���ɂ��鑼�̃v���C���[���擾
            Player[] otherPlayers = PhotonNetwork.PlayerListOthers;
            //���̃v���C���[�Ɋ��蓖�Ă��Ă���A�g���Ȃ����O��ID��ۑ����Ă����z����`
            var cantUseId = new List<string>();

            foreach (var pl in otherPlayers)
            {
                //���Ɏg���Ă���ID��ۑ����Ă���
                cantUseId.Add(pl.NickName);
            }

            //Player1�Ƃ������O�̃��[�U�[�����Ȃ���΁AID1���g�p����B
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
        //ID��o�^
        m_userSettingData.GetSetPlayerId = m_playerId;
        //�v���C���[��\��
        photonView.RPC(nameof(OnOffPlayerModel), RpcTarget.All);

        //�f�o�b�N
        Debug.Log("����������");
    }

    //�v���C���[��\������֐�
    [PunRPC]
    void OnOffPlayerModel()
    {
        //�Q�����Ă���v���C���[���\��
        foreach (var i in PhotonNetwork.PlayerList)
        {
            //�f�o�b�N
            Debug.Log("�v���C���[" + i.ActorNumber + "���Q����");

            //�Q�����Ă���v���C���\�̃��f����\��
            m_playerGameObject[i.ActorNumber-1].SetActive(true);

            //�Q�����Ă���v���C���[�̃e�L�X�g��\��
            GameObject.Find("PlayerName" + i.ActorNumber + "Text").GetComponent<Text>().enabled = true;
            //�Q�����Ă���v���C���[�̃e�L�X�g�����[�U�[���ɂ���
            GameObject.Find("PlayerName" + i.ActorNumber + "Text").GetComponent<Text>().text = PlayerPrefs.GetString("UserName", "NoName");
            //�Q�����Ă���v���C���[�̖�E�e�L�X�g��\��
            GameObject.Find("Player" + i.ActorNumber + "RoleText").GetComponent<Text>().enabled = true;
        }
    }

    //�Q�[���V�[���Ɉڍs����֐�
    [PunRPC]
    void GoGameScene()
    {
        //�Q�[���V�[���Ɉڍs
        SceneManager.LoadScene("GameScene");
    }
}