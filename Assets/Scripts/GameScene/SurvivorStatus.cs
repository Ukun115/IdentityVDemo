using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�o�C�o�[�̃X�e�[�^�X
/// </summary>
public class SurvivorStatus : MonoBehaviour
{
    //�̗�
    int m_hitPoint = 2;

    //�̗͑����֐�
    public void HitPointUpDown(int upDownNum)
    {
        m_hitPoint =+ upDownNum;

        //�̗͂�0�ɂȂ�����A
        if (m_hitPoint <= 0)
        {
            //0��菬�����l�ɂȂ��Ă邩������Ȃ��̂ŕ␳�����Ă���
            m_hitPoint = 0;

            //�_�E����Ԃɂ���
            this.GetComponent<SurvivorMovement>().GetSetIsDownStatu = true;

            //�f�o�b�N
            Debug.Log("�_�E����ԂɂȂ�܂����B");
        }
        else
        {
            //�_�E����Ԃ���������
            this.GetComponent<SurvivorMovement>().GetSetIsDownStatu = false;
        }

        //�f�o�b�N
        Debug.Log("�̗͂�" + m_hitPoint + "�ɂȂ�܂����B");
    }

}