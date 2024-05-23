using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheWindowGame.Enums;
using TheWindowGame.Extensions;
using TheWindowGame.Utilities;

namespace TheWindowGame.GameObjects;

public partial class Player : GameObject
{
    private readonly Utilities.InputManager _inputManager;
    private readonly WindowManager _windowManager;

    private const double _accelerationForce = 0.5d;
    private const double _frictionCoefficient = 0.03d;

    public Player() : base()
    {
        InitializeComponent();
        _inputManager = Utilities.InputManager.Instance;
        _windowManager = WindowManager.Instance;
    }

    protected override void Start()
    {
        Window mainWindow = Application.Current.MainWindow;
        Vector centerPos = new(mainWindow.Width / 2, mainWindow.Height / 2);
        Position = centerPos;

        base.Start();
    }

    protected override void Update()
    {
        if (!WallCollision())
        {
            // Handle inputs only when the player is inside the play area.
            Velocity *= 1 - _frictionCoefficient;     // Apply friction force.

            Vector force = _inputManager.GetInputDirection() switch
            {
                Direction.Up => new(0, -_accelerationForce),
                Direction.RightUp => new(_accelerationForce / 2, -_accelerationForce / 2),
                Direction.Right => new(_accelerationForce, 0),
                Direction.RightDown => new(_accelerationForce / 2, _accelerationForce / 2),
                Direction.Down => new(0, _accelerationForce),
                Direction.LeftDown => new(-_accelerationForce / 2, _accelerationForce / 2),
                Direction.Left => new(-_accelerationForce, 0),
                Direction.LeftUp => new(-_accelerationForce / 2, -_accelerationForce / 2),
                _ => new()
            };
            ApplyForce(force);
        }
        
        base.Update();
    }

    public override void Destroy()
    {
        base.Destroy();
        _inputManager.UnregisterEvents();
    }

    public bool WallCollision()
    {
        Size windowSize = _windowManager.GetMainWindow().GetActualSize();
        bool rightCollide = Position.X + Width >= windowSize.Width;
        bool bottomCollide = Position.Y + Height >= windowSize.Height;

        double playerVelocity = Math.Sqrt(Math.Pow(Velocity.X, 2) + Math.Pow(Velocity.Y, 2));
        if (Position.X <= 0d || rightCollide)     // Wall collision on x side.
        {
            double a = Math.Asin(Velocity.Y / playerVelocity);
            double velocityX = playerVelocity * Math.Cos(a) * (rightCollide ? -1 : 1);
            double velocityY = playerVelocity * Math.Sin(a);

            Velocity = new();
            ApplyForce(new(velocityX, velocityY));
        }
        else if (Position.Y <= 0d || bottomCollide)     // Wall collision on y side.
        {
            double a = Math.Asin(Velocity.X / playerVelocity);
            double velocityX = playerVelocity * Math.Sin(a);
            double velocityY = playerVelocity * Math.Cos(a) * (bottomCollide ? -1 : 1);

            Velocity = new();
            ApplyForce(new(velocityX, velocityY));
        }
        else
        {
            return false;
        }

        return true;
    }
}

