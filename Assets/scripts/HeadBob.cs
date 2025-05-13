using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField, Range(0.001f, 0.001f)]
    private float amount = 0.002f;
    [SerializeField, Range(1f, 30f)]
    private float frequency = 10.0f;
    [SerializeField, Range(10f, 100f)]
    private float smooth = 10.0f;

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadBobTrigger();
        StopHeadBob();
    }

    private void CheckForHeadBobTrigger(){
        float inputMagnitude = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")).magnitude;

        if (0 < inputMagnitude){
            StartHeadBob();
        }
    }
    private Vector3 StartHeadBob(){
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, (float)(Mathf.Sin(Time.time*frequency) * amount * 1.4), smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, (float)(Mathf.Cos(Time.time*frequency/2) * amount * 1.6), smooth * Time.deltaTime);
        transform.localPosition = pos;
        return pos;
    }
    private void StopHeadBob(){
        if (transform.localPosition == startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos,1 * Time.deltaTime);  
    }
}
