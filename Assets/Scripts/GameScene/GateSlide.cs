using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲートをスライドさせる処理
/// </summary>
public class GateSlide : MonoBehaviour
{
    //スライド方向が逆かどうか
    [SerializeField] int m_isReverse = 1;

    //スライドパワー
    float m_slidePower = 0.0f;

    //ゲートのスライドを開始していいか
    bool m_isStart = false;

    void Update()
    {
        //ゲートスライド開始
        if(m_isStart)
        {
            //スライドパワーを増やしていく
            m_slidePower += 0.00001f;

            //ゲートを移動させていく
            this.transform.position += new Vector3(m_slidePower* m_isReverse, 0.0f,0.0f);

            //開ききったら、
            if(m_slidePower > 0.0058f)
            {
                //スライドさせてるゲートオブジェクトを削除する
                Destroy(this.gameObject);
            }
        }
    }

    //ゲートのスライドを開始していいかを設定するセッター
    public void SlideStart()
    {
        m_isStart = true;
    }
}