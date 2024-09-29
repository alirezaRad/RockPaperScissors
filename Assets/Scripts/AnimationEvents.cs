using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private RpsGameManager gameManager;

    public void OnAnimationEnd()
    {
        gameManager.OnAnimationEnd();
    }
}
