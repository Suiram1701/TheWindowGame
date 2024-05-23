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
    private readonly Window _window;

    private readonly Collection<Key> _pressedKeys;

    public static InputManager Instance { get; }

    public InputManager() : this(Application.Current.MainWindow)
    {
    }

    public InputManager(Window window)
    {
        _window = window;

        _pressedKeys = [];
        _window.KeyDown += OnWindowKeyDown;
        _window.KeyUp += OnWindowKeyUp;
    }

    static InputManager()
    {
        Instance = new InputManager();
    }

    private void OnWindowKeyDown(object sender, KeyEventArgs e)
    {
        if (_pressedKeys.Contains(e.Key))
            return;
        _pressedKeys.Add(e.Key);
    }

    private void OnWindowKeyUp(object sender, KeyEventArgs e)
    {
        if (_pressedKeys.Contains(e.Key))
            _pressedKeys.Remove(e.Key);
    }

    /// <summary>
    /// Unregisters the events registered to some events of the window.
    /// </summary>
    public void UnregisterEvents()
    {
        _window.KeyDown -= OnWindowKeyDown;
        _window.KeyUp -= OnWindowKeyUp;
        _pressedKeys.Clear();
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
}