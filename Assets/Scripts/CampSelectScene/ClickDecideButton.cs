using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDecideButton : MonoBehaviour
{
    [SerializeField] GameObject m_realyOkBackImage = null;

    [SerializeField] GameObject[] m_decideButton = null;

    [SerializeField] GameObject m_randomNameDecideButton = null;

    [SerializeField] string m_selectCampName = "";

    //���͂��ꂽ���O
    [SerializeField] GameObject m_nameInputCharacter = null;

    //�{�^���������ꂽ�Ƃ��ɏ��������
    //�{���ɂ���ł������̊m�F�ֈڂ�
    public void NextRealyOk()
    {
        //�C���v�b�g�t�B�[���h�ɓ��͂��ꂽ���O���܂��Ȃ��Ƃ��͍ŏI�m�F�e�L�X�g��\�������Ȃ��B
        if(m_nameInputCharacter != null && m_nameInputCharacter.GetComponent<InputField>().text.Length == 0)
        {
            Debug.Log("���O�������͂ł��B");

            return;
        }

        m_realyOkBackImage.GetComponent<ClickRealyOkButton>().SetGetSelectCampName = m_selectCampName;

       //�ŏI�m�F�e�L�X�g��\��
        m_realyOkBackImage.SetActive(true);

       //����{�^�������������\��
       for (int i = 0; i < m_decideButton.Length; i++)
       {
           m_decideButton[i].SetActive(false);
       }

       if(m_randomNameDecideButton)
       {
            m_randomNameDecideButton.SetActive(false);
       }
    }
}