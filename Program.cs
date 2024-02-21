
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        const double G = 6.673e-11;

        Vettore v1 = new Vettore(0, 0);
        Vettore v2 = new Vettore(400e6, 0);
        Vettore v3 = new Vettore(400e6, 400e6);
        Vettore v4 = new Vettore(0, 400e6);

        Oggetto m1 = new Oggetto(6e24, v1);
        Oggetto m2 = new Oggetto(7e22, v2);
        Oggetto m3 = new Oggetto(7e22, v3);
        Oggetto m4 = new Oggetto(7e22, v4);

        double from1_2 = v1.Distanza(v2) * v1.Distanza(v2);
        double from1_3 = v1.Distanza(v3) * v1.Distanza(v3);
        double from1_4 = v1.Distanza(v4) * v1.Distanza(v4);

        double forza = G * ((m1.Massa * m2.Massa) / from1_2 + (m1.Massa * m3.Massa) / from1_3 + (m1.Massa * m4.Massa) / from1_4);

        Console.WriteLine($"Forza totale = {forza} N");

        Console.ReadKey();
    }
}

public class Oggetto
{
    public double Massa { get; set; }
    public Vettore Posizione { get; set; }

    public Oggetto(double massa, Vettore posizione)
    {
        Massa = massa;
        Posizione = posizione;
    }
}

public class Vettore
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vettore(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vettore Parse(string s)
    {
        string[] subs = s.Split(';');
        return new Vettore(double.Parse(subs[0]), double.Parse(subs[1]));
    }

    public static bool TryParse(string s, out Vettore v)
    {
        double x, y;
        string[] subs = s.Split(';');
        if (subs.Length > 1 && double.TryParse(subs[0], out x) && double.TryParse(subs[1], out y))
        {
            v = new Vettore(x, y);
            return true;
        }
        else
        {
            v = null;
            return false;
        }
    }

    public double Distanza(Vettore other)
    {
        double dx = other.X - X;
        double dy = other.Y - Y;
        double distanzainclinata = Math.Sqrt(dx * dx + dy * dy);
        return distanzainclinata;
    }

    public static double Risultante(Vettore v)
    {
        return Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
    }

    public static Vettore operator +(Vettore v)
    {
        return v;
    }

    public static Vettore operator -(Vettore v)
    {
        return new Vettore(-v.X, -v.Y);
    }

    public static Vettore operator +(Vettore v1, Vettore v2)
    {
        return new Vettore(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vettore operator -(Vettore v1, Vettore v2)
    {
        return new Vettore(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vettore operator *(Vettore v1, Vettore v2)
    {
        return new Vettore(v1.X * v2.X, v1.Y * v2.Y);
    }
    public static Vettore operator *(Vettore v1, double s)
    {
        return new Vettore(v1.X * s, v1.Y * s);
    }
    public static Vettore operator *(double s, Vettore v2)
    {
        return new Vettore(s * v2.X, s * v2.Y);
    }

    public static Vettore operator /(Vettore v1, double s)
    {
        return new Vettore(v1.X / s, v1.Y / s);
    }

    public static bool operator ==(Vettore v1, Vettore v2)
    {
        if (ReferenceEquals(v1, null))
        {
            return ReferenceEquals(v2, null);
        }
        else if (ReferenceEquals(v2, null))
        {
            return false;
        }
        else
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }
    }

    public static bool operator !=(Vettore v1, Vettore v2)
    {
        return !(v1 == v2);
    }

    public override string ToString()
    {
        return string.Format("{0};{1}", X, Y);
    }
}