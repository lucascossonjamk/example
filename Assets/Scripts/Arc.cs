using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform target;
    public float height = 10f;
    private Vector3 start;
    private Coroutine coroutine;

    private void Awake()
    {
        start = transform.position;
    }

    private void Update()
    {
        if (coroutine == null)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                coroutine = StartCoroutine(Curve());
            }
        }
    }

    IEnumerator Curve()
    {
        float duration = 1f;
        float time = 0f;

        Vector3 end = target.position - (target.forward * 0.55f); // lead the target a bit to account for travel time, your math will vary

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float h = Mathf.Lerp(0f, height, heightT); // change 3 to however tall you want the arc to be

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, h);

            yield return null;
        }

        // impact

        coroutine = null;
    }
}
