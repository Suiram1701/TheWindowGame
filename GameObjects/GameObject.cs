using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TheWindowGame.Utilities;

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

    protected readonly ObjectManager _objectManager;

    protected GameObject()
    {
        _objectManager = ObjectManager.Instance;

        Position = new();
        Velocity = new();
        Acceleration = new();
        Mass = 1.0d;
    }

    /// <summary>
    /// Once called when the object initializes.
    /// </summary>
    public virtual void Start()
    {
        Canvas.SetLeft(this, Position.X);
        Canvas.SetTop(this, Position.Y);
    }

    /// <summary>
    /// This get called on every in-game frame.
    /// </summary>
    public virtual void Update()
    {
        Velocity += Acceleration;
        Position += Velocity;

        Canvas.SetLeft(this, Position.X);
        Canvas.SetTop(this, Position.Y);
    }

    public virtual void Destroy()
    {
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