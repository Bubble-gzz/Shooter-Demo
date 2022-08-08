using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointBorderPiece : MonoBehaviour
{
    // Start is called before the first frame update
    AimPointCenter center;
    [SerializeField]
    Vector2 offset;
    void Start()
    {
        center = transform.parent.GetComponent<AimPointCenter>();
        offset = offset.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset * center.radius;
    }
}
