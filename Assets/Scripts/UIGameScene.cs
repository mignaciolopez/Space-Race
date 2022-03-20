using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameScene : MonoBehaviour
{
    [SerializeField] TMP_Text score1Text;
    [SerializeField] TMP_Text score2Text;

    private void OnGUI()
    {
        score1Text.text = GameManager.instance.score1.ToString();
        score2Text.text = GameManager.instance.score2.ToString();
    }
}
