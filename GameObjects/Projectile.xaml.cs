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
namespace TheWindowGame.GameObjects;

public partial class Projectile : GameObject
{
    public Projectile() : base()
    {
        InitializeComponent();
    }

    public override void Start()
    {
        Width = 10;
        Height = 10;

        base.Start();
    }
}
