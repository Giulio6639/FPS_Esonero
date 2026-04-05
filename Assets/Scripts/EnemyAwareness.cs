using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRange = 8f;
    public bool isAggro;
    public Material aggroMat;
    private Transform playersTransform;

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awarenessRange)
        {
            isAggro = true;
        }

        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
