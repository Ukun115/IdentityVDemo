using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//�}�b�`���O��ʂŖ�E�����肷�鏈��
public class DecideRole : MonoBehaviourPunCallbacks
{
    //���[�U�[���[��
    [SerializeField]string m_role = "���[����";

    //�v���C���[�̉��ɐݒu���Ă����E���e�L�X�g
    [SerializeField] Text[] m_playerRoleText = null;

    UserSettingData m_userSettingData = null;

    [SerializeField] RoleTable m_roleTable = null;

    void Start()
    {
        m_userSettingData = GameObject.Find("UserSettingData").GetComponent<UserSettingData>();
    }

    //�{�^���������ꂽ�Ƃ��ɑI�����Ă����E��ύX���鏈��
    public void OnClickRoleButton()
    {
        m_userSettingData.GetSetPlayerRole = m_role;
        //�e�L�X�g���X�V
        photonView.RPC(nameof(UpdateRoleText), RpcTarget.All, m_userSettingData.GetSetPlayerId, m_userSettingData.GetSetPlayerRole);

        //�f�o�b�N
        Debug.Log(m_userSettingData.GetSetPlayerRole + "�ɖ�E��ύX���܂����B");

        //��E�\�����
        m_roleTable.OnClickRoleTableButton();
    }

    //��E�e�L�X�g�X�V����
    [PunRPC]
    void UpdateRoleText(int playerId,string playerRole)
    {
        m_playerRoleText[playerId-1].text = playerRole;
    }
}