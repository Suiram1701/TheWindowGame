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
    public static Size GetActualSize(this Window window)
    {
        double width = window.Width - 16;
        double height = window.Height - 38;
        return new Size(width, height);
    }
}
