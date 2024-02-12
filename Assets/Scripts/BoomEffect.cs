using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    List<Transform> objs = new List<Transform>();

    const int N = 15;

    void Start()
    {
        for (int i=0; i<N; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(Mathf.Cos(i * 2 * Mathf.PI / N), 0, Mathf.Sin(i * 2 * Mathf.PI / N));
            obj.transform.forward = obj.transform.position - transform.position;
            objs.Add(obj.transform);
        }
    }

    void Update()
    {
        foreach (Transform trans in objs)
        {
            trans.Translate(0, 0, 10 * Time.deltaTime);
            trans.localScale *= 0.9f;

            if (trans.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
