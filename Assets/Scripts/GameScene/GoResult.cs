using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoResult : MonoBehaviour
{
    //���U���g�ɍs���邩�ǂ���
    [SerializeField]bool m_canGoResult = false;

    //���̃R���|�[�l���g�����t�����Ă���̂��n�b�`���ǂ���
    [SerializeField]bool m_isHatch = false;

    bool m_ehehe = false;

    void Update()
    {
        if (m_ehehe)
        {
            //�n�b�`�̎��̓L�[���͂�����
            if (m_isHatch)
            {
                //�X�y�[�X�L�[��������Ă��Ȃ��ƃ��U���g�ɍs�������͎��s���Ȃ�
                if (Input.GetKey(KeyCode.Space))
                {
                    //���U���g��ʂɑJ��

                    //�f�o�b�N
                    Debug.Log("���U���g��ʂɈڍs");

                    //���͉��Ńe�L�X�g�uWin�v�\��
                    GameObject.Find("ResultText").GetComponent<Text>().enabled = true;
                }
            }
            else
            {
                //���U���g��ʂɑJ��

                //�f�o�b�N
                Debug.Log("���U���g��ʂɈڍs");

                //���͉��Ńe�L�X�g�uWin�v�\��
                GameObject.Find("ResultText").GetComponent<Text>().enabled = true;
            }
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //�����Ă����I�u�W�F�N�g���T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //���U���g��ʂɍs����Ƃ��A
            if (m_canGoResult)
            {
                m_ehehe = true;

                //�n�b�`�̎��A
                if (m_isHatch)
                {
                    //�f�o�b�N
                    Debug.Log("�n�b�`�g�p�\");
                }
                //�Q�[�g�̎��A
                else
                {
                    //�f�o�b�N
                    Debug.Log("���U���g�J��");
                }
            }
            //�܂����U���g��ʂɍs���Ȃ��Ƃ��A
            else
            {
                //�n�b�`�̎��A
                if (m_isHatch)
                {
                    //�f�o�b�N
                    Debug.Log("�n�b�`�g�p�s��");
                }
                //�Q�[�g�̎��A
                else
                {
                    //�f�o�b�N
                    Debug.Log("���U���g�s��");
                }
            }
        }
    }

    //���U���g�ɍs���邩�ǂ�����ݒ肷��Z�b�^�[
    public void SetCanGoResult(bool canGoResult)
    {
        m_canGoResult = canGoResult;
    }
}