using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheWindowGame.Enums;

namespace TheWindowGame.Utilities;

/// <summary>
/// A manager of inputs
/// </summary>
internal class InputManager
{
    private readonly Collection<Key> _pressedKeys;
    private readonly Collection<MouseButton> _pressedMouseButtons;

    public static InputManager Instance { get; }

    public InputManager()
    {
        _pressedKeys = [];
        _pressedMouseButtons = [];
    }

    static InputManager()
    {
        Instance = new InputManager();
        Instance.RegisterEvents(WindowManager.Instance.GetMainWindow());
    }

    private void OnKeyChange(object sender, KeyEventArgs e)
    {
        if (e.KeyStates.HasFlag(KeyStates.Down))
        {
            if (!_pressedKeys.Contains(e.Key))
            {
                _pressedKeys.Add(e.Key);
            }
        }
        else
        {
            _pressedKeys.Remove(e.Key);
        }

        e.Handled = true;
    }

    private void OnMouseButtonChange(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _pressedMouseButtons.Add(MouseButton.Left);
        }
        else
        {
            _pressedMouseButtons.Remove(MouseButton.Left);
        }

        if (e.MiddleButton == MouseButtonState.Pressed)
        {
            _pressedMouseButtons.Add(MouseButton.Middle);
        }
        else
        {
            _pressedMouseButtons.Remove(MouseButton.Middle);
        }

        if (e.RightButton == MouseButtonState.Pressed)
        {
            _pressedMouseButtons.Add(MouseButton.Right);
        }
        else
        {
            _pressedMouseButtons.Remove(MouseButton.Right);
        }

        if (e.XButton1 == MouseButtonState.Pressed)
        {
            _pressedMouseButtons.Add(MouseButton.XButton1);
        }
        else
        {
            _pressedMouseButtons.Remove(MouseButton.XButton1);
        }

        if (e.XButton2 == MouseButtonState.Pressed)
        {
            _pressedMouseButtons.Add(MouseButton.XButton2);
        }
        else
        {
            _pressedMouseButtons.Remove(MouseButton.XButton2);
        }

        e.Handled = true;
    }

    public void RegisterEvents(Window window)
    {
        window.KeyDown += OnKeyChange;
        window.KeyUp += OnKeyChange;
        window.MouseDown += OnMouseButtonChange;
        window.MouseUp += OnMouseButtonChange;
    }

    public void UnregisterEvents(Window window)
    {
        window.KeyDown -= OnKeyChange;
        window.KeyUp -= OnKeyChange;
        window.MouseDown -= OnMouseButtonChange;
        window.MouseUp -= OnMouseButtonChange;

        _pressedKeys.Clear();
        _pressedMouseButtons.Clear();
    }

    /// <summary>
    /// Gets the direction from the user input.
    /// </summary>
    /// <returns></returns>
    public Direction GetInputDirection()
    {
        bool upPressed = _pressedKeys.Contains(Key.Up);
        bool rightPressed = _pressedKeys.Contains(Key.Right);
        bool downPressed = _pressedKeys.Contains(Key.Down);
        bool leftPressed = _pressedKeys.Contains(Key.Left);

        if (upPressed && rightPressed)
            return Direction.RightUp;
        else if (downPressed && rightPressed)
            return Direction.RightDown;
        else if (downPressed && leftPressed)
            return Direction.LeftDown;
        else if (upPressed && leftPressed)
            return Direction.LeftUp;
        else if (upPressed)
            return Direction.Up;
        else if (rightPressed)
            return Direction.Right;
        else if (downPressed)
            return Direction.Down;
        else if (leftPressed)
            return Direction.Left;
        else
            return Direction.None;
    }

    /// <summary>
    /// Indicates whether the fire key is pressed.
    /// </summary>
    /// <returns>The result.</returns>
    public bool IsFirePressed()
    {
        return _pressedMouseButtons.Contains(MouseButton.Left);
    }
}