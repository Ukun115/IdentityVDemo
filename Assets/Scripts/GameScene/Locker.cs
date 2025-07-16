using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���b�J�[�̓��ޏ���
public class Locker : MonoBehaviour
{
    //�����\���ǂ���
    bool m_canEnter = false;

    //���
    enum EnLockerStatu
    {
        enEnter,             //������
        enEnterPlayerStop,   //����̂Ƀv���C���[�������~�߂Ă�����
        enIn,                //�����Ă�����
        enExitPlayerStop,    //�o�����ɊO�Ńv���C���[�������~�߂Ă�����
    }
    EnLockerStatu m_lockerStatu = EnLockerStatu.enEnter;

    //�A�����ďo���肵�Ȃ��悤�ɒx����������^�C�}�[
    int m_delayTimer = 60;
    //�x������
    int m_delayTime = 72;

    void Update()
    {
        switch (m_lockerStatu)
        {
            //������
            case EnLockerStatu.enEnter:

                //�x���^�C�}�[���v������
                if (m_delayTimer < m_delayTime)
                {
                    m_delayTimer++;
                }

                //�����\�Ȏ��A
                if (m_canEnter && m_delayTimer == m_delayTime)
                {
                    //�X�y�[�X�L�[�������ꂽ��A
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //�X�e�[�g�`�F���W
                        m_lockerStatu = EnLockerStatu.enEnterPlayerStop;

                        //�v���C���[�̓������~�߂�
                        GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(true);

                        //�x���^�C�}�[��������
                        m_delayTimer = 0;

                        //�f�o�b�N
                        Debug.Log("���b�J�[�ɓ���");
                    }
                }
                break;

            //����̂Ƀv���C���[�������~�߂Ă�����
            case EnLockerStatu.enEnterPlayerStop:
                //�x���^�C�}�[���v������
                m_delayTimer++;
                if (m_delayTimer > m_delayTime)
                {
                    //���b�J�[�̒��ɓ���

                    //Enter�|�C���g�Ƀv���C���[���΂�
                    GameObject.Find("Survivor").transform.position = GameObject.Find("EnterPoint").transform.position;

                    //�X�e�[�g�`�F���W
                    m_lockerStatu = EnLockerStatu.enIn;
                    //�x���^�C�}�[��������
                    m_delayTimer = 0;
                }
                break;

            //�����Ă�����
            case EnLockerStatu.enIn:

                //�x���^�C�}�[���v������
                if (m_delayTimer < m_delayTime)
                {
                    m_delayTimer++;
                }

                if (m_delayTimer == m_delayTime)
                {
                    //�X�y�[�X�L�[�������ꂽ��A
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //���b�J�[����o��

                        //�X�e�[�g�`�F���W
                        m_lockerStatu = EnLockerStatu.enExitPlayerStop;

                        //Exit�|�C���g�Ƀv���C���[���΂�
                        GameObject.Find("Survivor").transform.position = GameObject.Find("ExitPoint").transform.position;

                        //�x���^�C�}�[��������
                        m_delayTimer = 0;

                        //�f�o�b�N
                        Debug.Log("���b�J�[����o��");
                    }
                }
                break;

            //�o�����ɊO�Ńv���C���[�������~�߂Ă�����
            case EnLockerStatu.enExitPlayerStop:
                //�x���^�C�}�[���v������
                m_delayTimer++;
                if (m_delayTimer > m_delayTime)
                {
                    //�X�e�[�g�`�F���W
                    m_lockerStatu = EnLockerStatu.enEnter;
                    //�x���^�C�}�[��������
                    m_delayTimer = 0;

                    //�v���C���[�̓������ĊJ������
                    GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(false);
                }
                    break;
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //�T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //�f�o�b�N
            Debug.Log("���b�J�[���o�\");

            //�����\�ɂ���
            m_canEnter = true;
        }
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //�T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //�f�o�b�N
            Debug.Log("���b�J�[���o�s��");

            //�����s�ɂ���
            m_canEnter = false;
        }
    }
}