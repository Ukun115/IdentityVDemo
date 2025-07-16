using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// ���������A�������A�{�^�����򏈗�
/// </summary>
public class ReadyOkButton : MonoBehaviourPunCallbacks
{
    //�{�^�����
    bool m_isReadyOk = false;
    //���������{�^���̃e�L�X�g
    [SerializeField]Text m_readyText = null;
    //���������l��
    int m_readyOkPlayer = 0;
    //���������l���e�L�X�g
    [SerializeField] Text m_waitPlayerText = null;
    //�`�F�b�N�}�[�N�摜
    [SerializeField] Image[] m_checkMarkImage = null;
    //
    UserSettingData m_userSettingData = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();
    }

    //�{�^�������ꂽ��{�^����Ԃ�ؑւ�
    public void OnClickReadyButton()
    {
        //������t�ɂ���
        m_isReadyOk = !m_isReadyOk;

        //����������Ԃ̂Ƃ��A
        if(m_isReadyOk)
        {
            m_readyText.text = "�����L�����Z��";

            //���������l����+1����
            photonView.RPC(nameof(AddReadyOkPlayer), RpcTarget.All, m_userSettingData.GetSetPlayerId-1);
        }
        //�������̎��A
        else
        {
            m_readyText.text = "��������";

            //���������l����-1����
            photonView.RPC(nameof(ReduceReadyOkPlayer), RpcTarget.All, m_userSettingData.GetSetPlayerId-1);
        }
    }

    //���������l���̃v���p�e�B
    public int GetSetmReadyOkPlayerNumber
    {
        get {return m_readyOkPlayer; }
        set { m_readyOkPlayer = value; }
    }

    [PunRPC]
    void AddReadyOkPlayer(int id)
    {
        m_readyOkPlayer++;
        //���������l���e�L�X�g���X�V
        m_waitPlayerText.text = m_readyOkPlayer + "/5";
        //�`�F�b�N�}�[�N���I��
        m_checkMarkImage[id].enabled = true;
    }
    [PunRPC]
    void ReduceReadyOkPlayer(int id)
    {
        m_readyOkPlayer--;
        //���������l���e�L�X�g���X�V
        m_waitPlayerText.text = m_readyOkPlayer + "/5";
        //�`�F�b�N�}�[�N���I�t
        m_checkMarkImage[id].enabled = false;
    }
}