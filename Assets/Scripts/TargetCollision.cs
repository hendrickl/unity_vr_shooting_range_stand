using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCollision : MonoBehaviour
{
    private int _headScore = 0;
    private int _bodyScore = 0;

    [SerializeField] private TMP_Text _headScoreText;
    [SerializeField] private TMP_Text _bodyScoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BodyTarget"))
        {
            _bodyScore++;
            _bodyScoreText.text = _bodyScore.ToString();
        }

        if (gameObject.CompareTag("HeadTarget"))
        {
            _headScore++;
            _headScoreText.text = _headScore.ToString();
        }
    }
}
