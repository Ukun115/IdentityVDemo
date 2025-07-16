using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Í��@�̉�ǐi�x
/// </summary>
public class Decrypt : MonoBehaviourPunCallbacks
{
    //��ǐi�x
    float m_decryptProgress = 0.0f;

    //��Ǒ��x
    float m_codeMachineSpeed = 0.5f;  //�Í��@
    float m_gateSwitchSpeed = 5.0f;    //�Q�[�g�X�C�b�`

    //��ǒ����ǂ���
    bool m_isDecoding = false;

    //��ǐi�x�o�[
    Image m_decryptProgressBarImage = null;

    [SerializeField]TouchCodeMachine m_touchCodeMachine = null;

    void Update()
    {
        //��ǒ�
        if (m_isDecoding)
        {
            //�Í��@�̉�ǐi�x��i�߂�
            if (m_touchCodeMachine.GetmIsTouchingTargetCodeMachine())
            {
                //���s�������֐����A���[�����̑S�������s����A�֐��ɓn����������
                photonView.RPC(nameof(DecryptAdvance),RpcTarget.All, m_codeMachineSpeed);
            }
            //�Q�[�g�X�C�b�`�̉�ǐi�x��i�߂�
            else
            {
                //���s�������֐����A���[�����̑S�������s����A�֐��ɓn����������
                photonView.RPC(nameof(DecryptAdvance), RpcTarget.All, m_gateSwitchSpeed);
            }

            //�ړ��L�[�������ꂽ��A
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)||
               Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //��ǒ�����Ȃ�����B
                ChangeIsDecoding(false);

                //��ǊJ�n�ł����Ԃɖ߂��Ă���
                m_touchCodeMachine.SetCanStartDecrypt(true);

                //�f�o�b�N
                Debug.Log("��ǒ��f");
            }

            //��ǐi�x�o�[��L�΂��Ă���
            m_decryptProgressBarImage.rectTransform.sizeDelta = new Vector2(m_decryptProgress*1.5f,10);
        }
        //��ǐi�x��100���ɂȂ�����A
        if (m_decryptProgress >= 100.0f && this.tag != "Untagged")
        {
            //��Ǌ���

            //�^�O�������čēx�G��Ȃ��悤�ɂ���
            this.tag = "Untagged";

            //��ǒ�����Ȃ�����B
            ChangeIsDecoding(false);

            //��Ǎς݂̈Í��@�͉��F�����Ă����B
            this.GetComponent<Renderer>().material.color = Color.yellow;

            //�Í��@����ǂ��Ă�����A
            if (m_touchCodeMachine.GetmIsTouchingTargetCodeMachine())
            {
                //�c���ǐ���-1����
                GameObject.Find("DontDecryptCodeYetText").
                GetComponent<ChangeLimitDecodingNumber>().
                    ReduceLimitDecodingNumber();

                //�f�o�b�N
                Debug.Log("�Í��@��Ǌ���");
            }
            //�Q�[�g�X�C�b�`����ǂ��Ă�����A
            else
            {
                //�f�o�b�N
                Debug.Log("�Q�[�g�X�C�b�`��Ǌ���");

                //�Q�[�g�P�̂Ƃ��A
                if (this.gameObject.name == "GateSwitch1")
                {
                    GameObject.Find("Gate_Right1").GetComponent<GateSlide>().SlideStart();
                    GameObject.Find("Gate_Left1").GetComponent<GateSlide>().SlideStart();
                }
                //�Q�[�g�Q�̂Ƃ��A
                if (this.gameObject.name == "GateSwitch2")
                {
                    GameObject.Find("Gate_Right2").GetComponent<GateSlide>().SlideStart();
                    GameObject.Find("Gate_Left2").GetComponent<GateSlide>().SlideStart();
                }
            }
        }
    }

    //��ǒ����ǂ�����؂�ւ��鏈��
    public void ChangeIsDecoding(bool flag)
    {
        m_isDecoding = flag;

        //�u�Í���ǁv�e�L�X�gUI�̕\���𔽓]
        GameObject.Find("CodeDecryptText").GetComponent<Text>().enabled = flag;

        //��ǐi�x�o�[�̕\���𔽓]
        GameObject.Find("DecryptProgressBackImage").GetComponent<Image>().enabled = flag;
        m_decryptProgressBarImage = GameObject.Find("DecryptProgressImage").GetComponent<Image>();
        m_decryptProgressBarImage.enabled = flag;
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //�Í��@�̐ڐG�\�͈͂���T�o�C�o�[�����ꂽ��A
        //���A���̂Ƃ���ǒ���������A
        if (other.gameObject.tag == "Survivor" && m_isDecoding)
        {
            //��ǒ�����Ȃ�����B
            ChangeIsDecoding(false);
        }
    }

    //��ǂ�i�߂�֐�
    [PunRPC]
    void DecryptAdvance(float decryptSpeed)
    {
        m_decryptProgress += decryptSpeed;
        //�f�o�b�N
        Debug.Log(m_decryptProgress);
    }
}