using TLE.DataTypes;

namespace TLE.ScreenManagement;

public class GraphicsManager
{
    private string[] screen;
    private byte _urgency;
    private List<List<CharObject>> activeObjects;

    public GraphicsManager()
    {
        activeObjects = new List<List<CharObject>>();
        
        for (int i = 0; i < 5; i++)
        {
            activeObjects.Add(new List<CharObject>());
        }

        screen = new[]
        {
            "                              ",
            "                              ",
            "                              ",
            "                              ",
            "                              ",
            "                              ",
            "                              ",
            "                              "
        };
    }
    private void RefreshScreen(Byte urge)
    {
        _urgency += urge;

        if (_urgency >= 5)
        {
            Console.Clear();
            for (int i = 0; i < screen.Length; i++)
            {
                Console.WriteLine(screen[i]);
            }
        }
    }

    private void WriteScreen()
    {
        foreach (var layer in activeObjects)
        {
            foreach (var obj in layer)
            {
                if (obj.isActive)
                {
                    Test(obj.sprite,obj.GetPosition());
                }
            }
        }
        
        RefreshScreen(5);
    }

    private void Test(string[] sprite, Vector2 pos)
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            if (pos.y + i < 0)
            {
                break;
            }
            char[] buffer = screen[pos.y + i].ToCharArray();
           // Console.WriteLine("Buffer = " + new string(buffer));

            for (int j = 0; j < sprite[i].Length; j++)
            {
                if (pos.x + j < 0)
                {
                    break;
                }

             //   Console.WriteLine(i + " <-> " + j);
               
                if (sprite[i][j] != ' ')
                {
                    if (sprite[i][j] == '^')
                    {
                        buffer[j] = ' ';
                    }
                    else
                    {
                        buffer[j] = sprite[i][j];
                    }
                }
            }

            screen[pos.y + i] = new string(buffer);
            
        //    RefreshScreen(5);
        }
    }
    
    public void Print(CharObject obj, int layer)
    {
        activeObjects[layer].Add(obj);
    }

    public void Print(CharObject obj)
    {
        activeObjects[0].Add(obj);
       // RefreshScreen(5);
        WriteScreen();
    }
}

/*
 Arquitetura baseada em prioridade
toda atualização de tela envia também seu nível de urgência
para evitar piscadas no terminal, quando a soma das urgencias
na fila de espera atingir um certo numero ou após X segundos,
 a tela é atualizada
*/