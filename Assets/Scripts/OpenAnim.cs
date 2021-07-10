using UnityEngine;
using DG.Tweening;

public class OpenAnim : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOScale(0, .5f).From();
    }
}