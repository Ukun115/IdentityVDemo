using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;      //�ǉ�
using Photon.Realtime; //�ǉ�

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;
    UserSettingData m_userSettingData = null;
    //�T�o�C�o�[�̃v���t�@�u
    [SerializeField] GameObject m_survivorPrefab;
    //�n���^�[�̃v���t�@�u
    [SerializeField] GameObject m_hunterPrefab;
    //�v���C���[���e�L�X�g
    [SerializeField] Text[] m_playerNameText = null;
    //�v���C���[���[���e�L�X�g
    [SerializeField] Text[] m_playerRoleText = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();

        //�v���C���[���ƃ��[����\��
        photonView.RPC(nameof(ActivePlayerNameAndRole), RpcTarget.All, m_userSettingData.GetSetPlayerId,PlayerPrefs.GetString("UserName", "NoName"));

        //�T�o�C�o�[���I�΂�Ă�����A
        if (m_userSettingData.GetSetIsSurvivorCamp)
        {
            m_gameObject = PhotonNetwork.Instantiate(
            m_survivorPrefab.name,
            new Vector3(0f, 1f, 0f),    //�|�W�V����
            Quaternion.identity,        //��]
            0
            );
            //��������Q�[���I�u�W�F�N�g�̖��O��Survivor�ɂ���
            m_gameObject.name = "Survivor";
        }
        //�n���^�[���I�΂�Ă�����A
        else
        {
            m_gameObject = PhotonNetwork.Instantiate(
            m_hunterPrefab.name,
            new Vector3(0f, 1f, 0f),    //�|�W�V����
            Quaternion.identity,        //��]
            0
            );
            //��������Q�[���I�u�W�F�N�g�̖��O��Hunter�ɂ���
            m_gameObject.name = "Hunter";
        }

        //�J�������I���ɂ���(�g�p����)
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("MiniMapCamera").GetComponent<Camera>().enabled = true;

        //�f�o�b�N
        Debug.Log("���[�U�[���́u" + PlayerPrefs.GetString("UserName", "NoName") + "�v�ł�");
        Debug.Log("��E�́u" + m_userSettingData.GetSetPlayerRole + "�v�ł�");
    }

    [PunRPC]
    //�v���C���[���ƃ��[����\�������鏈��
    void ActivePlayerNameAndRole(int id,string name)
    {
        //�v���C���[����\��
        m_playerNameText[id-1].text = name;
        //�v���C���[���[����\��
        m_playerRoleText[id-1].text = "�u" + m_userSettingData.GetSetPlayerRole + "�v";
    }
}