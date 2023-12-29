using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour {
    public Text debugInfoUI;
    public bool isDebugModeEnabled;
    
    private float deltaTime;
    private StringBuilder stringBuilder;
    
    // Start is called before the first frame update
    void Start() {
        stringBuilder = new StringBuilder("FPS: ", 5);
    }
    
    void Update() {
        if (isDebugModeEnabled) {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            
            stringBuilder.Clear();
            stringBuilder.Append("FPS: ").Append(fps.ToString("F2"));
            debugInfoUI.text = stringBuilder.ToString();
            
            debugInfoUI.gameObject.SetActive(true);
        }
        else debugInfoUI.gameObject.SetActive(false);
    }
}
