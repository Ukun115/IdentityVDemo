using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n���^�[����j�󂷂鏈��
/// </summary>
public class BoardBreak : MonoBehaviour
{
    //��j��ł��邩�ǂ���
    bool m_canBreak = false;

    //��j�󂵂Ă����Ԃ��ǂ���
    bool m_isBreak = false;

    //�j��ɂ����鎞��
    [SerializeField] int m_boardBreakTime = 0;
    float m_boardBreakTimer = 0.0f;

    //�n���^�[�̃Q�[���I�u�W�F�N�g
    GameObject m_hunter = null;

    private void Update()
    {
        //�^�u�L�[�������ꂽ�Ƃ��A
        if (Input.GetKey(KeyCode.Tab)&& !m_isBreak)
        {
            //��|�����ԂȂ�A
            if (m_canBreak)
            {
                //�j�󏈗��Ɉڍs
                m_isBreak = true;

                //�f�o�b�N
                Debug.Log("�j��J�n");
                Debug.Log("�����鎞�ԁF" + m_boardBreakTime);

                //�n���^�[�̓������~�߂�
                m_hunter.GetComponent<HunterMovement>().SetIsStop(true);
            }
        }

        //��j�󂵂Ă���Ƃ��̏���
        if(m_isBreak)
        {
            //�^�C�}�[���o�߂����Ă���
            m_boardBreakTimer += 0.1f;

            //�f�o�b�N
            Debug.Log(m_boardBreakTimer);

            //���Ԍo�߂�����A
            if (m_boardBreakTimer > m_boardBreakTime)
            {
                //�f�o�b�N
                Debug.Log("�j�󊮗�");

                //�n���^�[�̓������Đ�������
                m_hunter.GetComponent<HunterMovement>().SetIsStop(false);

                //�I�u�W�F�N�g��j��
                Destroy(GameObject.Find("Board").gameObject);

            }
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //���̃X�N���v�g���Ă΂�Ă��Ȃ���������s���Ȃ�
        if(!this.enabled)
        {
            return;
        }

        //�n���^�[��������A
        if (other.gameObject.tag == "Hunter")
        {
            //���󂹂��Ԃɂ���
            m_canBreak = true;

            m_hunter = other.gameObject;

            //�f�o�b�N
            Debug.Log("�j��\���");
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

        //�n���^�[��������A
        if (other.gameObject.tag == "Hunter")
        {
            //���󂹂Ȃ���Ԃɂ���
            m_canBreak = false;

            m_hunter = other.gameObject;

            //�f�o�b�N
            Debug.Log("�j��s���");
        }
    }
}