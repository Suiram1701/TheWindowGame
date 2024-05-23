using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheWindowGame.Windows;

namespace TheWindowGame.Utilities;

internal class WindowManager
{
    private readonly Application _application;

    public static WindowManager Instance { get; }

    static WindowManager()
    {
        Instance = new();
    }

    public WindowManager()
    {
        _application = Application.Current;
    }

    public MainWindow GetMainWindow()
    {
        return _application.Windows.OfType<MainWindow>().First();
    }
}
