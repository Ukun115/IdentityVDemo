using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �c���ǐ�
/// </summary>
public class ChangeLimitDecodingNumber : MonoBehaviour
{
    //�c���ǐ�
    int m_limitDecodingNumber = 5;

    //�c���ǐ������炷����
    public void ReduceLimitDecodingNumber()
    {
        //�c���ǐ������炷
        m_limitDecodingNumber--;

        //�e�L�X�g�R���|�[�l���g���擾���A
        //�c���ǐ����X�V����
        if (m_limitDecodingNumber != 0)
        {
            this.GetComponent<Text>().text = m_limitDecodingNumber + "�̈Í����܂���ǂ���Ă��܂���";

            //�Í��@�̉�ǂ��Q��I�������A
            if(m_limitDecodingNumber == 3)
            {
                //�n�b�`�̃e�L�X�g
                GameObject.Find("HatchText").GetComponent<Text>().text = "�n�������X�V�ς݂ł�";

                //�n�b�`���g�p�\�ɂ���
                GameObject.Find("HitArea").GetComponent<GoResult>().SetCanGoResult(true);
            }
        }
        //�Í��@5����Ǌ��������Ƃ��A
        else
        {
            this.GetComponent<Text>().text = "�T�o�C�o�[�͓d���X�C�b�`�����邱�Ƃ��ł��܂�";
        }
    }

    //�c���ǐ����擾����Q�b�^�[
    public int GetLimitDecodingNumber()
    {
        return m_limitDecodingNumber;
    }
}