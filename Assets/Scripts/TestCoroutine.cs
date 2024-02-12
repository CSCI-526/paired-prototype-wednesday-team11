using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    public GameObject prefab;
    void Start()
    {
        // 开启协程
        StartCoroutine(CreateCube());
    }

    // IEnumerator：迭代器
    IEnumerator CreateCube()
    {
        for (int i=0; i<20; i++)
        {
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject cube = Instantiate(prefab);
            cube.transform.position = new Vector3(i, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cube.transform.position = new Vector3(i, 0, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    int index = 0;

    void Update()
    {
        //if (index < 20)
        //{
        //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    cube.transform.position = new Vector3(index, 0, 0);
        //    index++;

        //}
    }
}
