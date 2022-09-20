public delegate void OnKeyPress(bool isPress);
public delegate float OnAxisTouch(float value);

public interface IInputSystem
{
    bool GetKeyDown(string keyName);
    bool GetKey(string keyName);
    bool GetKeyUp(string keyName);
    float GetAxis(string axisName);
    bool GetMouseButtonDown(string keyName);
    bool GetMouseButton(string keyName);
    bool GetMouseButtonUp(string keyName);
}