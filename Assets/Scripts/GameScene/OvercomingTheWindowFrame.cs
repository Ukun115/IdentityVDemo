using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���g���z������
/// </summary>
public class OvercomingTheWindowFrame : MonoBehaviour
{
    //���g���z���\���ǂ�������
    bool m_canWindowFrameJump = false;

    //�v���C���[�ƃW�����v�|�C���g�Ƃ̋���
    float[] m_playerJumpPointLength = { 0.0f,0.0f };

    //�W�����v��̔ԍ�(���l�̓W�����v�|�C���g�̔ԍ�)
    int m_jumpNumber = 0;

    //���g�̏��
    enum EnWindowFrameState
    {
        enJumpBefore,   //���g��щz���O
        enJumping       //���g��щz����
    }
    EnWindowFrameState m_windowFrameStage = EnWindowFrameState.enJumpBefore;

    //�g��щz���|�C���g
    [SerializeField]GameObject[] m_frameJumpPoint = { null };
    //�v���C���[
    GameObject m_player = null;

    //�d���ҋ@�^�C�}�[
    int m_waitTimer = 0;

    //�d������
    [SerializeField]int m_initWaitTime = 0;
    int m_waitTime = 0;

    void Start()
    {
        //�d�����Ԃ̏����l��ۑ�
        m_waitTime = m_initWaitTime;
    }

    void Update()
    {
        //���g�̏�Ԃɂ���ď�����ύX
        switch (m_windowFrameStage)
        {
            //���g��щz���O
            case EnWindowFrameState.enJumpBefore:
                //���g���z���\�̂Ƃ��A
                if (m_canWindowFrameJump)
                {
                    //�X�y�[�X�L�[�������ꂽ�Ƃ��A
                    //���A�_�E����Ԃ���Ȃ��Ƃ��A
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //�T�o�C�o�[�̎��A
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            //�_�E����Ԃ̂Ƃ��͎��s���Ȃ�
                            if(m_player.GetComponent<SurvivorMovement>().GetSetIsDownStatu)
                            {
                                return;
                            }
                        }

                        //�ǂ����̃W�����v�|�C���g�ɍs��������

                        //�W�����v�|�C���g0��1�ƃv���C���[�̋������擾
                        for (int jumpPointNum = 0; jumpPointNum < 2; jumpPointNum++)
                        {
                            m_playerJumpPointLength[jumpPointNum] = Vector3.Distance(m_frameJumpPoint[jumpPointNum].transform.position, m_player.transform.position);
                        }
                        //�W�����v�|�C���g0�̂ق����W�����v�|�C���g1�����߂�������A
                        if (m_playerJumpPointLength[0] < m_playerJumpPointLength[1])
                        {
                            //�v���C���[�̓W�����v�|�C���g1�ɔ��
                            m_jumpNumber = 1;
                        }
                        else
                        {
                            //�v���C���[�̓W�����v�|�C���g0�ɔ��
                            m_jumpNumber = 0;
                        }

                        //�v���C���[�̓������~�߂�
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            m_player.GetComponent<SurvivorMovement>().SetIsStop(true);
                        }
                        if (m_player.gameObject.tag == "Hunter")
                        {
                            m_player.GetComponent<HunterMovement>().SetIsStop(true);
                        }

                        //�v���C���[�̈ړ���Ԃɂ���čd�����Ԃ�ύX
                        //����ȊO�͍d�����Ԃ𒷂�����
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            if (m_player.GetComponent<SurvivorMovement>().GetMovementStatu() != 1)
                            {
                                m_waitTime *= 2;

                                //�f�o�b�N
                                Debug.Log("�x����щz��");
                            }
                            //����ړ���Ԃ̎��A
                            else
                            {
                                m_waitTime = m_initWaitTime;

                                //�f�o�b�N
                                Debug.Log("������щz��");
                            }
                        }
                        if (m_player.gameObject.tag == "Hunter")
                        {
                            m_waitTime *= 3;

                            //�f�o�b�N
                            Debug.Log("�x����щz��");
                        }

                        //��щz�������Ɉڍs
                        m_windowFrameStage = EnWindowFrameState.enJumping;
                    }
                }
                break;

            //���g��щz����
            case EnWindowFrameState.enJumping:

                //�d���ҋ@�^�C�}�[�����Z����
                m_waitTimer++;

                //��щz���O�̍d��
                if (m_waitTimer == m_waitTime)
                {
                    //�f�o�b�N
                    Debug.Log(m_jumpNumber + "�Ԃɔ�т܂�");
                    //�v���C���[���W�����v�|�C���g�Ƀ��[�v������
                    m_player.transform.position = m_frameJumpPoint[m_jumpNumber].transform.position;
                }

                //��щz����̍d��
                if (m_waitTimer > m_waitTime*2)
                {
                    //���g��щz���O�̏����ɖ߂�
                    m_windowFrameStage = EnWindowFrameState.enJumpBefore;
                    //�d���ҋ@�^�C�}�[��������
                    m_waitTimer = 0;
                    //�d�����Ԃ�������
                    m_waitTime = m_initWaitTime;

                    //�v���C���[�̓������ĊJ������
                    if (m_player.gameObject.tag == "Survivor")
                    {
                        m_player.GetComponent<SurvivorMovement>().SetIsStop(false);
                    }
                    if(m_player.gameObject.tag == "Hunter")
                    {
                        m_player.GetComponent<HunterMovement>().SetIsStop(false);
                    }
                }

                break;
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //���̃X�N���v�g���Ă΂�Ă��Ȃ���������s���Ȃ�
        if (!this.enabled)
        {
            return;
        }

        //�T�o�C�o�[or�n���^�[��������A
        if (other.gameObject.tag == "Survivor"|| other.gameObject.tag == "Hunter")
        {
            //���g���z���\��Ԃɂ���
            m_canWindowFrameJump = true;

            m_player = other.gameObject;

            //�f�o�b�N
            Debug.Log("���g���z���\���");
        }
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //���̃X�N���v�g���Ă΂�Ă��Ȃ���������s���Ȃ�
        if (!this.enabled)
        {
            return;
        }

        //�T�o�C�o�[or�n���^�[��������A
        if (other.gameObject.tag == "Survivor" || other.gameObject.tag == "Hunter")
        {
            //���g���z���s��Ԃɂ���
            m_canWindowFrameJump = false;

            m_player = other.gameObject;

            //�f�o�b�N
            Debug.Log("���g���z���s���");
        }
    }
}