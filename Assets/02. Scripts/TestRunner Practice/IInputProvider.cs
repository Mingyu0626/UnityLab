using UnityEditor;
using UnityEngine;

public interface IInputProvider
{
    public bool IsKeyPressed(KeyCode keycode);
}
