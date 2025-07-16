using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickRealyOkButton : MonoBehaviour
{
    //���͂��ꂽ�e�L�X�g
    [SerializeField] Text[] m_inputText = null;

    //����{�^��
    [SerializeField] GameObject[] m_decideButton = null;

    [SerializeField] GameObject m_realyOkBackImage = null;

    [SerializeField] GameObject m_randomNameDecideButton = null;

    //���̃V�[����
    [SerializeField] string m_nextSceneName = "";

    string m_selectCampName = "";

    //�͂�
    public void Yes()
    {
        //�ݒ肵�����[�U�[����o�^���Ă���
        for (int i = 0; i < m_inputText.Length; i++)
        {
            //�v���C���[�v���t�X�ɓo�^
            PlayerPrefs.SetString("UserName",m_inputText[i].text.ToString());
            PlayerPrefs.Save();
        }

        if (m_selectCampName == "�T�o�C�o�[")
        {
            //�I�΂ꂽ�w�c��ۑ�
            GameObject.Find("UserSettingData").GetComponent<UserSettingData>().GetSetIsSurvivorCamp = true;
        }
        if (m_selectCampName == "�n���^�[")
        {
            //�I�΂ꂽ�w�c��ۑ�
            GameObject.Find("UserSettingData").GetComponent<UserSettingData>().GetSetIsSurvivorCamp = false;
        }

        //�V�[���ɑJ��
        SceneManager.LoadScene(m_nextSceneName);
    }

    //������
    public void No()
    {
        //�ŏI�m�F�{�^�����\��
        m_realyOkBackImage.SetActive(false);

        //����{�^����\��
        for (int i = 0; i < m_decideButton.Length; i++)
        {
            m_decideButton[i].SetActive(true);
        }

        if(m_randomNameDecideButton)
        {
            m_randomNameDecideButton.SetActive(true);
        }
    }

    public string SetGetSelectCampName
    {
        set { m_selectCampName = value; }
        get { return m_selectCampName; }
    }
}
