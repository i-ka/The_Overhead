using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour
{

    public float seconds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(destroy(seconds));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
