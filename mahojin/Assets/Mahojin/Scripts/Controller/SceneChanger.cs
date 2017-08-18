using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンを切り替える機能
/// </summary>
public class SceneChanger : MonoBehaviour {
    [SerializeField] private SceneController.SceneKind changeToKind;

    /// <summary>
    /// ChangeToKindに切り替える
    /// </summary>
    public void Change()
    {
        SceneController.I.LoadAsync(changeToKind);
    }
}