using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Í��@�Ƃ̐ڐG����
/// </summary>
public class TouchCodeMachine : MonoBehaviour
{
    //��ǊJ�n�ł��邩�ǂ���
    bool m_canStartDecrypt = false;

    //���������Ă���Ώۂ̈Í��@�I�u�W�F�N�g
    GameObject m_codeMachine = null;

    //�G�����̂��Í��@���ǂ����̔���
    bool m_isTouchingTargetCodeMachine = true;

    void Update()
    {
        //��ǊJ�n�ł���Ƃ��A
        if(m_canStartDecrypt)
        {
            //�X�y�[�X�L�[�������ꂽ��A
            if (Input.GetKey(KeyCode.Space))
            {
                //��ǊJ�n

                //�f�o�b�N
                Debug.Log("��ǊJ�n");

                //��ǒ��ɂ���
                m_codeMachine.GetComponent<Decrypt>().ChangeIsDecoding(true);

                //��ǊJ�n�ł��Ȃ�����ɂ���
                m_canStartDecrypt = false;
            }
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //�Í��@�̐ڐG�\�͈͂ɓ����Ă�����A
        //���A�c���ǐ���0����Ȃ��Ƃ��A
        if (other.gameObject.tag == "CodeMachine" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() != 0)
        {
            //�f�o�b�N
            Debug.Log("�Í��@��ǉ\���");

            //�ǂ̈Í��@����ۑ�
            m_codeMachine = other.gameObject;

            //��ǊJ�n�ł��锻��ɂ���
            m_canStartDecrypt = true;

            //�G���Ă���̂��Í��@�I
            m_isTouchingTargetCodeMachine = true;
        }
        //�Q�[�g�X�C�b�`�̐ڐG�\�͈͂ɓ����Ă�����A
        //���A�c���ǐ���0�̂Ƃ��A
        if(other.gameObject.tag == "GateSwitch"&&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() == 0)
        {
            //�f�o�b�N
            Debug.Log("�Q�[�g�X�C�b�`��ǉ\���");

            //�ǂ̈Í��@����ۑ�
            m_codeMachine = other.gameObject;

            //��ǊJ�n�ł��锻��ɂ���
            m_canStartDecrypt = true;

            //�G���Ă���̂��Í��@����Ȃ��I
            m_isTouchingTargetCodeMachine = false;
        }
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //�Í��@�̐ڐG�\�͈͂��痣�ꂽ��A
        //���A�c���ǐ���0����Ȃ��Ƃ��A
        if (other.gameObject.tag == "CodeMachine" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() != 0)
        {
            //�f�o�b�N
            Debug.Log("�Í��@��Ǖs���");

            //��ǊJ�n�ł��Ȃ�����ɂ���
            m_canStartDecrypt = false;
        }
        //�Q�[�g�X�C�b�`�̐ڐG�\�͈͂ɓ����Ă�����A
        //���A�c���ǐ���0�̂Ƃ��A
        if (other.gameObject.tag == "GateSwitch" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() == 0)
        {
            //�f�o�b�N
            Debug.Log("�Q�[�g�X�C�b�`��Ǖs���");

            //��ǊJ�n�ł��Ȃ�����ɂ���
            m_canStartDecrypt = false;
        }
    }

    //��ǊJ�n�ł��邩�ǂ�����ݒ肷��Z�b�^�[
    public void SetCanStartDecrypt(bool flag)
    {
        m_canStartDecrypt = flag;
    }

    //�G���Ă���̂��Í��@���ǂ������Q�b�g����Q�b�^�[
    public bool GetmIsTouchingTargetCodeMachine()
    {
        return m_isTouchingTargetCodeMachine;
    }
}
