using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointRotation : MonoBehaviour
{
    [Range(0f, 1f)]
    public float rotationValue;
    Quaternion rotate;

    public IEnumerator Rotate()
    {
        transform.Rotate(0, 0, rotationValue * 15 * 0.2f * (PlayerBasic.positionY - transform.position.y));
        yield return new WaitForSeconds(0.05f);
        transform.Rotate(0, 0, 0);
        yield return new WaitForSeconds(0.05f);

    }
    void RotateFuction()
    {

        StartCoroutine(Rotate());
    }
    

    private void Update()
    {
        rotate.Set(0, 0, rotationValue * 15 * 0.2f * (PlayerBasic.positionY - transform.position.y), 0);
        transform.rotation = rotate;
    }
}
