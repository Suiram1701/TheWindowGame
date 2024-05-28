using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheWindowGame.GameObjects;
using TheWindowGame.Windows;

namespace TheWindowGame.Utilities;

public class ObjectManager
{
    private readonly WindowManager _windowManager;

    public static ObjectManager Instance { get; }

    public ObjectManager()
    {
        _windowManager = WindowManager.Instance;
    }

    static ObjectManager()
    {
        Instance = new();
    }

    public TObject AddObject<TObject>(TObject obj, Vector position, Vector velocity)
        where TObject : GameObject
    {
        obj.Position = position;
        obj.Velocity = velocity;

        _windowManager.GetMainWindow().gameArea.Children.Add(obj);
        obj.Start();

        return obj;
    }

    public void RemoveObject(GameObject obj)
    {
        obj.Destroy();
        _windowManager.GetMainWindow().gameArea.Children.Remove(obj);
    }
}
