namespace Juego
{
    public class ClassicScore: IGetScore{
        public ClassicScore()
        {
        }
        public int Score(Token token){
            return token.Part1.GetHashCode() + token.Part2.GetHashCode();
        }
    }

    public class DoubleScore: IGetScore{
        public int Score(Token token){
            int result = token.Part1+token.Part2;
            if (token.IsDouble()) result *=2;
            return result;
        }
    }
}