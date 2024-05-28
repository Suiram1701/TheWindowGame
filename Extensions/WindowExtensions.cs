using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheWindowGame.Extensions;

internal static class WindowExtensions
{
    /// <summary>
    /// Get the actual size of the window without the bar on the top.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns>The actual size.</returns>
    public static Size GetWindowSize(this Window window)
    {
        // use largest available size.
        double width = (window.ActualWidth > window.Width ? window.ActualWidth : window.Width) - 16;
        double height = (window.ActualHeight > window.Height ? window.ActualHeight : window.Height) - 38;
        return new Size(width, height);
    }
}
