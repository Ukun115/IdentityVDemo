using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����_���Ŗ��O�����肷�鏈��
/// </summary>
public class RandomDecideButton : MonoBehaviour
{
    //���O���
    [SerializeField]string[] m_randomName = null;

    [SerializeField]InputField m_inputText = null;

    //�{�^���������ꂽ�烉���_���Ŗ��O���R���Ă��鏈��
    public void OnClickDecideRandomName()
    {
        //���O��\��
        m_inputText.text = m_randomName[Random.Range(0, m_randomName.Length)];

        //�f�o�b�N
        Debug.Log("���肳�ꂽ���O�F" + m_inputText.text);
    }
}