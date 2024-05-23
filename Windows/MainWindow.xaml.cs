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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TheWindowGame.Windows;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;

    public MainWindow()
    {
        InitializeComponent();

        int interval = ((App)Application.Current).Interval;
        _timer = new(TimeSpan.FromMilliseconds(interval), DispatcherPriority.Render, OnTimerTick, Dispatcher);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {

    }
}
