using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region oldscript
    /*
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
    */
    #endregion

    public float duration;
    public float magnitude;

    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed+=Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
