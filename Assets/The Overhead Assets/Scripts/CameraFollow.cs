using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float marginX;
    [SerializeField]
    private float marginY;

    void Start () {
        Vector3 newPos = transform.position;
        newPos.x = target.position.x;
        newPos.y = target.position.y;
        transform.position = newPos;
    }

    bool CheckX()
    {
        return Mathf.Abs(transform.position.x-target.position.x)>marginX;
    }
    bool CheckY()
    {
        return Mathf.Abs(transform.position.y - target.position.y) > marginY;
    }

    void TrackTarget()
    {
        Vector3 newPos = transform.position;
        if (CheckX())
        {
            newPos.x=Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*4);
        }
        if (CheckY())
        {
            newPos.y = Mathf.Lerp(transform.position.y, target.position.y, Time.deltaTime*5);
        }
        transform.position = newPos;
    }

	void Update () {
        TrackTarget();
    }
}
