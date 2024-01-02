using System;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static event EventHandler LeftClick;
    public static event EventHandler SpacebarPress;
    public static event EventHandler TabPress;
    
    protected virtual void OnLeftClick(EventArgs e)
    {
        LeftClick?.Invoke(this, e);
    }
    
    protected virtual void OnSpacebarPress(EventArgs e)
    {
        SpacebarPress?.Invoke(this, e);
    }
    
    protected virtual void OnTabPress(EventArgs e)
    {
        TabPress?.Invoke(this, e);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            OnLeftClick(EventArgs.Empty);
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnSpacebarPress(EventArgs.Empty);
        }
        
        if (Input.GetKeyDown(KeyCode.Tab)) {
            OnTabPress(EventArgs.Empty);
        }
    }
}
