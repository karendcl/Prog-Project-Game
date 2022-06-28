using System.Diagnostics;
namespace Game;


public class Token : ICloneable<Token>
{
    public virtual int Part1 { set; get; }
    public virtual int Part2 { set; get; }


    public Token(int Part1, int Part2)
    {
        this.Part1 = Part1;
        this.Part2 = Part2;
    }

    public override string ToString()
    {
        return "[" + Part1 + "|" + Part2 + "] ";
    }



    public bool Contains(int a)
    {
        return (this.Part1.Equals(a) || this.Part2.Equals(a));
    }

    public bool IsDouble()
    {
        return (this.Part1.Equals(this.Part2));
    }

    public void SwapToken()
    {
        var temp = this.Part1;
        this.Part1 = this.Part2;
        this.Part2 = temp;
    }

    public Token Clone()
    {
        return new Token(this.Part1, this.Part2);
    }
}


public class RandomToken : Token
{
    private int Part1Original => base.Part1;
    private int Part2Original => base.Part2;

    public override int Part1 { get { return Part_1(); } }
    public override int Part2 { get { return Part_2(); } }

    private readonly int MaxDouble;

    private readonly int porcent;
    public RandomToken(int Part1, int Part2, int MaxDouble, int porcent) : base(Part1, Part2)
    {
        this.MaxDouble = MaxDouble;
        this.porcent = porcent;
    }

    private int RandomC()
    {
        Random random = new Random();
        int x = random.Next(0, 10000);
        return x;
    }
    private int Part_1()
    {
        int x = RandomC();

        if (x > porcent * 100)
        {
            Random random = new Random();
            int ran = random.Next(0, MaxDouble);
            return ran;
        }

        return Part1Original;
    }

    private int Part_2()
    {
        int x = RandomC();

        if (x > porcent * 100)
        {
            Random random = new Random();
            int ran = random.Next(0, MaxDouble);
            return ran;
        }

        return Part2Original;
    }
}


/*

public class Pinguino : IComparable<Pinguino>, ITokenizable
{
    public string Description { get; set; } = "Pinguino";
    public int altura
    { get; private set; }
    public int peso { get; private set; }

    public double ComponentValue { get; protected set; }

    public int age { get; private set; }
    Stopwatch watch = new Stopwatch();

    public Pinguino(int Altura, int Peso)
    {
        this.peso = peso;
        this.altura = altura;
        watch.Start();
    }

    private int AgeNow()
    {
        return (int)watch.ElapsedMilliseconds * 20000;
    }

    public double ComponentValueMethod()
    {
        return age * peso * altura / Math.E;
    }

    public int CompareTo(Pinguino? other)
    {
        if (this.age > other.age) return 1;
        if (this.age < other.age) return -1;
        return 0;
    }


}
*/

/*
public class EnergyGenerator : ITokenizable<EnergyGenerator>
{
    public string name { get; set; }

    public int minPotenci { get; protected set; }
    public int maxPotenci { get; protected set; }

    public string Description { get => "Termoeléctrica"; }

    public double ComponentValue => PotencialKnow();

    string ITokenizable<EnergyGenerator>.Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public double PotencialKnow()
    {
        Random random = new Random();
        int x = random.Next(-1, 1);
        return x * random.Next(minPotenci, maxPotenci);

    }



    public EnergyGenerator(string name, int minPotenci, int maxPotenci)
    {
        this.name = name;
        this.minPotenci = minPotenci;
        this.maxPotenci = maxPotenci;

    }

}*/


public class TokenVector<T>
{
    //El generador se asegura que todos tengan igual cant de componentes como el de mayor dimension
    public List<T> Component { get; set; }

    public int Dimension { get; set; }

}







