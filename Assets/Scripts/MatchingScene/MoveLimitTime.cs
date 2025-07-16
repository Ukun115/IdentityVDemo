using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// �}�b�`���O��ʂ̎c�莞�Ԃ�i�߂鏈��
/// </summary>
public class MoveLimitTime : MonoBehaviourPunCallbacks
{
    //�b
    float m_seconds = 60.5f;
    //��
    int m_minutes = 2;
    //�������ԃe�L�X�g
    [SerializeField] Text m_limitTimeText = null;

    void Update()
    {
        //�z�X�g�̂ݎ��s
        if (PhotonNetwork.IsMasterClient)
        {
            //���Ԃ��J�E���g�_�E������
            m_seconds -= Time.deltaTime;

            //�b��0�ɂȂ�����A
            if(m_seconds <= 0.0f)
            {
                //�b��������
                m_seconds = 60.0f;
                //����-1����
                m_minutes--;
                //�f�o�b�N
                Debug.Log("�P���o��");
            }

            //���Ԃ�S�v���C���[�ɕ\������
            photonView.RPC(nameof(UpdateLimitTimeText), RpcTarget.All, m_minutes, m_seconds);
        }
    }

    //���Ԃ�S�v���C���[�ɕ\������
    [PunRPC]
    void UpdateLimitTimeText(int minute,float second)
    {
        //���Ԃ�\������

        //�b���P���̏ꍇ
        if (second < 10.0f)
        {
            m_limitTimeText.text = "0" + minute + ":0" + second.ToString("f0");
        }
        //�Q���̏ꍇ
        else
        {
            m_limitTimeText.text = "0" + minute + ":" + second.ToString("f0");
        }

        return;
    }
}