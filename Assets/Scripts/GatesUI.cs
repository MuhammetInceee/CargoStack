using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GatesUI : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public int combunumber;

    Vector3 scale = new Vector3(0.015f, 0.015f, 0.015f);

    private void Update()
    {
        text.text = combunumber.ToString() + " $";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collected"))
        {
            text.gameObject.SetActive(true);
            Boing();
            combunumber++;
        }
    }
    
    void Boing()
    {
        text.rectTransform.DOScale(scale, 0.2f).OnComplete(() =>
        text.rectTransform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.2f));
    }
}
