using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サバイバーのステータス
/// </summary>
public class SurvivorStatus : MonoBehaviour
{
    //体力
    int m_hitPoint = 2;

    //体力増減関数
    public void HitPointUpDown(int upDownNum)
    {
        m_hitPoint =+ upDownNum;

        //体力が0になったら、
        if (m_hitPoint <= 0)
        {
            //0より小さい値になってるかもしれないので補正を入れておく
            m_hitPoint = 0;

            //ダウン状態にする
            this.GetComponent<SurvivorMovement>().GetSetIsDownStatu = true;

            //デバック
            Debug.Log("ダウン状態になりました。");
        }
        else
        {
            //ダウン状態を解除する
            this.GetComponent<SurvivorMovement>().GetSetIsDownStatu = false;
        }

        //デバック
        Debug.Log("体力が" + m_hitPoint + "になりました。");
    }

}