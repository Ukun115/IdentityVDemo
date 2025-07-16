using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageText : MonoBehaviour
{
    //ステージ名
    [SerializeField] string[] m_stageName = { "" };
    //ステージ名テキスト
    [SerializeField] Text m_stageNameText = null;
    //ランダムで決定したステージ番号
    int m_stageNumber = 0;

    void Start()
    {
        //ステージをランダムで決定
        m_stageNumber = Random.Range(0, m_stageName.Length);
        m_stageNameText.text = m_stageName[m_stageNumber];
    }
}