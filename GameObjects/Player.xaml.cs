using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private readonly ObjectManager _objManager;
    private readonly Utilities.InputManager _inputManager;
    private readonly WindowManager _windowManager;

    private const double _accelerationForce = 0.5d;
    private const double _frictionCoefficient = 0.03d;
    private const int _fireCooldown = 200;
    private const double _projectileVelocity = 50;

    private DateTime _lastShoot;
    private double _aimRad;

    public Player() : base()
    {
        InitializeComponent();
        _inputManager = Utilities.InputManager.Instance;
        _windowManager = WindowManager.Instance;
        _objManager = ObjectManager.Instance;

        Start();
    }

    public override void Start()
    {
        Size windowSize = _windowManager.GetMainWindow().GetWindowSize();
        Vector centerPos = new(windowSize.Width / 2, windowSize.Height / 2);
        Position = centerPos;

        _lastShoot = DateTime.Now;

        base.Start();
    }

    public override void Update()
    {
        // Handle inputs only when the player is inside the play area.
        if (!WallCollision())
        {
            UpdateInputForce();
        }

        UpdateAimIndicator();

        if (_inputManager.IsFirePressed() && (_lastShoot.AddMilliseconds(_fireCooldown) <= DateTime.Now))
        {
            Shoot();
        }
        
        base.Update();
    }

    private bool WallCollision()
    {
        Size windowSize = _windowManager.GetMainWindow().GetWindowSize();
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

    private void UpdateInputForce()
    {
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

    private void UpdateAimIndicator()
    {
        Point position = Mouse.GetPosition(this);
        position.X -= Width / 2;
        position.Y -= Height / 2;

        double rad = Math.Atan(position.Y / position.X);
        if (position.X < 0)
        {
            rad += Math.PI;
        }

        _aimRad = rad;
        SetAimIndicator(rad);
    }

    private void SetAimIndicator(double rad)
    {
        double angle = 180 * rad / Math.PI + 90;
        double rotationCircle = Height / 2 + 7;
        double indicatorX = rotationCircle * Math.Cos(rad);
        double indicatorY = rotationCircle * Math.Sin(rad);

        TransformGroup transformsGroup = (TransformGroup)aimIndicator.RenderTransform;
        ((RotateTransform)transformsGroup.Children[2]).Angle = angle;
        TranslateTransform translate = (TranslateTransform)transformsGroup.Children[3];
        translate.X = indicatorX;
        translate.Y = indicatorY;
    }

    private void Shoot()
    {
        double velocityX = Math.Cos(_aimRad) * _projectileVelocity;
        double velocityY = Math.Sin(_aimRad) * _projectileVelocity;
        Vector velocity = new(velocityX, velocityY);
        velocity += Velocity;

        Projectile projectile = new();
        double rotationCircle = Height / 2 + 7;
        double positionX = rotationCircle * Math.Cos(_aimRad) + Position.X + Width / 2 - projectile.Width / 2;
        double positionY = rotationCircle * Math.Sin(_aimRad) + Position.Y + Height / 2 - projectile.Height / 2;

        _objManager.AddObject(projectile, new(positionX, positionY), velocity);
        _lastShoot = DateTime.Now;
    }
}