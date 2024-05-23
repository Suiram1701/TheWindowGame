using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TheWindowGame.GameObjects;

/// <summary>
/// The base of every game object.
/// </summary>
public abstract class GameObject : UserControl
{
    /// <summary>
    /// The position of the object
    /// </summary>
    public Vector Position { get; set; }

    /// <summary>
    /// The velocity of the object.
    /// </summary>
    public Vector Velocity { get; set; }

    /// <summary>
    /// The acceleration of the object.
    /// </summary>
    public Vector Acceleration { get; set; }

    /// <summary>
    /// The mass of the object
    /// </summary>
    public double Mass { get; set; }

    private readonly DispatcherTimer _timer;

    protected GameObject()
    {
        Position = new();
        Velocity = new();
        Acceleration = new();
        Mass = 1.0d;

        int interval = ((App)Application.Current).Interval;
        _timer = new(TimeSpan.FromMilliseconds(interval), DispatcherPriority.Render, OnTimerTick, Dispatcher);

        Start();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        Update();
    }

    /// <summary>
    /// Once called when the object initializes.
    /// </summary>
    protected virtual void Start()
    {
        Canvas.SetLeft(this, Position.X);
        Canvas.SetTop(this, Position.Y);
    }

    /// <summary>
    /// This get called on every in-game frame.
    /// </summary>
    protected virtual void Update()
    {
        Velocity += Acceleration;
        Position += Velocity;

        Canvas.SetLeft(this, Position.X);
        Canvas.SetTop(this, Position.Y);
    }

    /// <summary>
    /// Destroys the object.
    /// </summary>
    public virtual void Destroy()
    {
        _timer.Stop();
    }

    /// <summary>
    /// Applies a force to this object.
    /// </summary>
    /// <param name="force">The force</param>
    public void ApplyForce(Vector force)
    {
        Acceleration = force / Mass;
    }
}