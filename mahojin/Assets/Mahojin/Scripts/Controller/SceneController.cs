using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを管理する機能
/// これをアタッチしたオブジェクトはシーンをまたいで生き続ける
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>{
    /// <summary>
    /// BuildSettingsに含まれるシーンの名前
    /// </summary>
    public enum SceneKind { Title, Puzzle, Edit }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadAsyncAllow(SceneKind load)
    {
        StartCoroutine(LoadScene(load));
    }

    private IEnumerator LoadScene(SceneKind load)
    {
        string loadstr = Enum.GetName(typeof(SceneKind),load);
        AsyncOperation async = SceneManager.LoadSceneAsync(loadstr);
        async.allowSceneActivation = false;
        
        //シーンロード完了を待って
        yield return new WaitUntil(() => async.progress >= 0.9f);

        //遷移
        async.allowSceneActivation = true;
    }
}
