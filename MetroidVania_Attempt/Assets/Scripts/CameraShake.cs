using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static bool shake;
    public AnimationCurve curve;
    public float duration;
    private Vector2 newPosition;

    private void Update()
    {
        if (shake) {
            shake = false;
            StartCoroutine(Shake()); 
        }
    }

    IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float startPositionX = transform.position.x;
        float startPositionY = transform.position.y;
        newPosition.Set(startPositionY, startPositionX);

        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = newPosition;
            yield return null;
            transform.position = startPosition;

        }
    }
}
