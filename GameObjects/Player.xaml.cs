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

namespace TheWindowGame.GameObjects;

public partial class Player : GameObject
{
    private readonly Utilities.InputManager _inputManager;

    private const double _accelerationForce = 0.5;

    public Player() : base()
    {
        InitializeComponent();
        _inputManager = new(Application.Current.MainWindow);
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
        Acceleration = force;

        base.Update();
    }

    public override void Destroy()
    {
        base.Destroy();
        _inputManager.UnregisterEvents();
    }
}

