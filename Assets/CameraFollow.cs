using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                          // seret PlayerCat kotak biru
    public Vector3 offset = new Vector3(0, 10, -7);
    public float smooth = 8f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(55, 0, 0);  // sudut miring tetap, ubah sesuai selera
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 goal = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, goal, smooth * Time.deltaTime);
    }
}