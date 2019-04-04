using System.Drawing;

public interface ICollision
{
    bool Collision(ICollision obj);
    Rectangle Rect { get;  }
}
