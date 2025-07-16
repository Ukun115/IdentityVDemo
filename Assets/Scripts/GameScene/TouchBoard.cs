using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBoard : MonoBehaviourPunCallbacks
{
    //���|�����Ԃ��ǂ���
    bool m_canBoardFalling = false;

    //��|���I����Ă��邩�ǂ���
    bool m_finishBoardFalling = false;

    //���щz����邩�ǂ���
    bool m_isBoardJumpStart = false;

    //�T�o�C�o�[�ƃW�����v�|�C���g�Ƃ̋���
    float[] m_playerJumpPointLength = { 0.0f, 0.0f };

    //�g��щz���|�C���g
    [SerializeField] GameObject[] m_frameJumpPoint = { null };
    //�T�o�C�o�[
    [SerializeField] GameObject m_survivor = null;

    void Update()
    {
        //�X�y�[�X�L�[�������ꂽ�Ƃ��A
        if (Input.GetKey(KeyCode.Space))
        {
            //��|�����ԂȂ�A
            if (m_canBoardFalling)
            {
                //�f�o�b�N
                Debug.Log("��|����");

                //�|���J�n����
                photonView.RPC(nameof(BoardFallingStart), RpcTarget.All);

                //
                //�T�o�C�o�[�̈ʒu���Z�b�g����
                //

                //�W�����v�|�C���g0��1�ƃT�o�C�o�[�̋������擾
                for (int jumpPointNum = 0; jumpPointNum < 2; jumpPointNum++)
                {
                    m_playerJumpPointLength[jumpPointNum] = Vector3.Distance(m_frameJumpPoint[jumpPointNum].transform.position, m_survivor.transform.position);
                }
                //�W�����v�|�C���g0�̂ق����W�����v�|�C���g1�����߂�������A
                if (m_playerJumpPointLength[0] < m_playerJumpPointLength[1])
                {
                    //�T�o�C�o�[�̓W�����v�|�C���g1�ɔ�΂���
                    m_survivor.transform.position = m_frameJumpPoint[0].transform.position;
                }
                else
                {
                    //�T�o�C�o�[�̓W�����v�|�C���g0�ɔ�΂���
                    m_survivor.transform.position = m_frameJumpPoint[1].transform.position;
                }
            }
            //��|���I�������Ԃ�������A
            //���A���щz������Ƃ��A
            if (GameObject.Find("BoardModel").GetComponent<Board>().GetSetBoardState == 2&&m_isBoardJumpStart)
            {
                //��щz������

                //���щz�����Ȃ�����
                m_isBoardJumpStart = false;

                //�f�o�b�N
                Debug.Log("���щz����");
            }
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //��|���I����Ă���Ƃ��͎��s���Ȃ�
        if(m_finishBoardFalling)
        {
            return;
        }

        //�����Ă����I�u�W�F�N�g���T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //��|�����Ԃɂ���
            m_canBoardFalling = true;

            //�f�o�b�N
            Debug.Log("�|���\���");
        }
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //��|���I����Ă���Ƃ��͎��s���Ȃ�
        if (m_finishBoardFalling)
        {
            return;
        }

        //�o���I�u�W�F�N�g���T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //��|���Ȃ���Ԃɂ���
            m_canBoardFalling = false;

            //�f�o�b�N
            Debug.Log("�|���s�\���");
        }
    }

    //���щz����邩�ǂ�����ݒ肷��Z�b�^�[
    public void SetBoardJumpStart(bool isBoardJumpStart)
    {
        m_isBoardJumpStart = isBoardJumpStart;
    }

    //�|���J�n�֐�
    [PunRPC]
    void BoardFallingStart()
    {
        //��|���A�j���[�V������ԂɑJ��
        GameObject.Find("BoardModel").GetComponent<Board>().GetSetBoardState = 1;
        //�|�ꂽ�̓����蔻������Ēʂ�Ȃ�����
        GameObject.Find("GuardCollider").GetComponent<BoxCollider>().isTrigger = false;
        //��|���Ȃ���Ԃɂ���
        m_canBoardFalling = false;
        //��|���I���������ɂ���
        m_finishBoardFalling = true;
    }
}