using UnityEngine;

public class Debug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.print(1.0f / Time.deltaTime);
    }
}
