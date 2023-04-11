using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private float timeToDisappear;
    private float currentAlpha;
    private Color alphaColor;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    void Update()
    {
        Color color = this.GetComponent<SpriteRenderer>().material.color;
        color.a -= Time.deltaTime * timeToDisappear;
        if(color.a <= 0f)
        {
            color.a = 0f;
        }
        this.GetComponent<SpriteRenderer>().material.color = color;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
