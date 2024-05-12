using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private MapManager _manager;

    public IEnumerator ScreenShake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        float min = -0.25f;
        float max = 0.25f;

        while (elapsed < duration)
        {
            float x = Random.Range(min, max) * magnitude;
            float y = Random.Range(min, max) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        _manager.TurnOnOverlay(true);
    }
}
