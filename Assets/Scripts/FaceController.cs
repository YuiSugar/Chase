using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [Header("振り向き処理（Animatorがアタッチされているオブジェクトにしか使えません）")]
    /// <summary> モデルにアタッチされているAnimator </summary>
    public Animator _ModelAnimator;

    [Header("振り向きフラグ（AnimatorのIK Passをオンにしておく）")]
    /// <summary> 振り向きをさせるかどうか </summary>
    public bool _IsLookAt = false;
    /// <summary> 振り向くオブジェクト（ターゲット） </summary>
    public GameObject _LookAtObj = null;
    /// <summary> 振り向く座標（ターゲット） </summary>
    public Vector3? _LookAtPos = null;
    /// <summary> 振り向くときの強さ（Animationの強さとの兼ね合い） </summary>
    public float _LookAtWeight = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// AnimatorIKを使ってボーンを操作する処理
    /// </summary>
    /// <param name="layerIndex">レイヤー番号</param>
    private void OnAnimatorIK(int layerIndex)
    {
        SetLookAt();
    }

    /// <summary>
    /// 振り向き処理
    /// </summary>
    private void SetLookAt()
    {
        
        if (_IsLookAt == false)
        {
            // 各ボーンに対しての動く重みを初期化する
            SetLookAtWeightZero();
            // 振り向き処理をしないときは処理を抜ける
            return;
        }

        if (_LookAtObj != null)
        {
            // 指定されたオブジェクトが入っていたらそこから座標を取得する
            _LookAtPos = (Vector3)_LookAtObj.transform.position;
        }

        if (_LookAtPos == null)
        {
            // 各ボーンに対しての動く重みを初期化する
            SetLookAtWeightZero();
            // 座標が入力されていないので、目標地点がないので処理しない
            return;
        }

        // 各ボーンに対しての動く重みを設定する
        _ModelAnimator.SetLookAtWeight(
            1.0f * _LookAtWeight,   // 全体の重み
            0.5f * _LookAtWeight,   // 上半身を動かす重み
            0.8f * _LookAtWeight,   // 頭を動かす重み
            1.0f * _LookAtWeight,   // 目を動かす重み
            0.0f                    // モーションの制限量（後で数値を入れる）
        );
        // 振り向く位置を設定する
        _ModelAnimator.SetLookAtPosition((Vector3)_LookAtPos);
    }

    /// <summary>
    /// 各ボーンに対しての動く重みを初期化する
    /// </summary>
    private void SetLookAtWeightZero() {
        _ModelAnimator.SetLookAtWeight(
            0.0f,   // 全体の重み
            0.0f,   // 上半身を動かす重み
            0.0f,   // 頭を動かす重み
            0.0f,   // 目を動かす重み
            0.0f    // モーションの制限量（後で数値を入れる）
        );
    }
}
