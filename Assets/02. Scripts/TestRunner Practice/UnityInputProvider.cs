using UnityEngine;

public class UnityInputProvider : IInputProvider
{
    public bool IsKeyPressed(KeyCode keycode)
    {
        return Input.GetKeyDown(keycode);
    }
}
