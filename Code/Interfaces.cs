using System.Collections;

namespace Game;
#region  Enums

public enum Orders
{
    NextPartida,

    NextPlay

}

#endregion
#region Rules

public interface IWinCondition<TCriterio, TToken> : IDescriptible
{                           //List<IPlayer> players
    List<Player> Winner(List<TCriterio> criterios, IGetScore<TToken> howtogetscore);
}

public interface IValidPlay<TGame, TPlayer, TCriterio> : IDescriptible
{
    TCriterio ValidPlay(TGame game, TPlayer player);


}

public interface IStopGame<TCriterio, TToken>
{                    // Puede ser un jugador 
                     // Puede ser un Juego para parar el torneo
    bool MeetsCriteria(TCriterio criterio, IGetScore<TToken> howtogetscore);
}


public interface IGetScore<TToken> : IDescriptible
{   //Puede ser un itoken o un juego
    double Score(TToken item);//hacer uno en torneo que sume todo lo de los metodos
}

#endregion


#region Game

public interface IPlayerStrategy : IDescriptible
{
    int Evaluate(IToken itoken, List<IToken> hand, WatchPlayer watch);
    int ChooseSide(ChooseStrategyWrapped choose, WatchPlayer watch);
}


public interface IPlayerScore : IDescriptible, IEquatable<PlayerScore>, IEquatable<int>, ICloneable<IPlayerScore>
{
    string Description { get; }
    double Score { get; }

    int PlayerId { get; }
    void AddScore(double score);
    bool Equals(PlayerScore? other);
    void LessScore(double score);
    void resetScore();
    void SetScore(double score);
    bool AddRange(IPlayerScore player);


}

public interface IJudge<TCriterio, TToken, TWrapped> : IDescriptible
{
    IStopGame<TCriterio, TToken> stopcriteria { get; set; }
    IWinCondition<TCriterio, TToken> winCondition { get; set; }

    IValidPlay<TCriterio, TToken, TWrapped> valid { get; set; }
    public IGetScore<IToken> howtogetscore { get; set; }

    bool ValidPlay(TCriterio criterio, TToken itoken);
    bool EndGame(Game game);

    int PlayerScore(Player player);

    bool ValidSettings(int TokensForEach, int MaxDoble, int players);

    void AddTokenTCriterio(IToken itoken, Board board, int side);
    //Añadir al board
    //Añadir a un partido un juegador
}



#endregion

#region  IToken
public interface ITokenizable : IComparable<ITokenizable>, IEquatable<ITokenizable>, IDescriptible
{
    string Paint();

    double ComponentValue { get; }

}

public interface IGenerator
{
    public List<IToken> CreateTokens(int maxDouble);
}


public interface ITokenManager
{
    protected int TokensForEach { get; set; }
    public List<IToken> Elements { get; protected set; }

    public GamePlayerHand<IToken> AssignTokens(Player player);
    public IComparer<IToken> Comparer { get; }
}






//Los criterios pueden darse en base a jugador o juego 


public interface ITokenizable<T> where T : IEnumerable<T>, IEquatable<T>, IDescriptible
{
    string Paint();

    double ComponentValue { get; }

    public string Description { get; }
}





public interface IToken
{
    ITokenizable Part1 { get; }
    ITokenizable Part2 { get; }

    IToken Clone();
    bool Contains(int a);


    bool ItsDouble();
    bool IsMatch(IToken other);
    void SwapToken();
    string ToString();
}

public interface ITokensManager
{
    List<IToken> Elements { get; }
    IEqualityComparer<IToken> equalityComparer { get; }
    IComparer<IToken> Comparer { get; }
    List<IToken> GetTokens();
    bool ItsDouble(IToken itoken);
}


#endregion


#region Auxiliar

public interface IDescriptible
{
    public static string Description { get; }
}
public interface ICloneable<T> : ICloneable
{
    new T Clone();
    Object ICloneable.Clone() => Clone()!;
}

public interface ICloneable<T1, T2> : ICloneable<T1>
{
    T1 Clone(T2 item);

}



#endregion



public interface ICorruptible
{
    bool Corrupt(double ScoreCost);

}



#region  Añadir ahora
#region  Game
public interface IJudgeGame
{
    bool AddTokenToBoard(Player player, GamePlayerHand<IToken> hand, IToken token, Board board, int side);
    bool EndGame(List<(Player, List<IToken>)> players, Board board);
    double PlayerScore(Player player);
    List<IPlayerScore> PlayersScores();
    WatchPlayer RunWatchPlayer(Board board);
    ChooseStrategyWrapped ValidPlay(Player player, Board board, IToken token);
    List<Player> Winner(List<(Player player, List<IToken> hand)> players);
}
#endregion

#region  Torenro
public interface IChampionJudge
{
    void AddFinishGame(Game game);
    bool EndGame(List<Game> game);
    double PlayerScore(int playerId);
    void Run(List<Player> players);
    bool ValidPlay(Player player);
    List<Player> Winners();
}
#endregion
public interface IPlayer : ICloneable<IPlayer>, IEquatable<IPlayer>, IEqualityComparer<IPlayer>, IDescriptible, IEquatable<int>
{
    List<IToken> hand { get; }
    int Id { get; }
    int TotalScore { get; set; }
    IPlayerStrategy strategy { get; }
    List<IPlayerStrategy> strategias { get; }

    void AddHand(List<IToken> Tokens);
    void AddStrategy(IPlayerStrategy strategy);
    IToken BestPlay(WatchPlayer watchPlayer);
    int ChooseSide(ChooseStrategyWrapped choose, WatchPlayer watchPlayer);
    IPlayer Clone();
    bool Equals(IPlayer? other);
    bool Equals(IPlayer? x, IPlayer? y);
    bool Equals(int otherId);
    int GetHashCode(IPlayer obj);
    string ToString();
}


public interface IBoard : ICloneable<IBoard>
{
    List<IToken> board { get; }
    IToken First { get; }
    IToken Last { get; }

    void AddTokenToBoard(IToken itoken, int side);
    IBoard Clone();
    IBoard Clone(List<IToken> CopyTokens);
    string ToString();
}

public interface IGame
{
    IBoard? board { get; }
    List<Player>? player { get; }

    Game Clone();
    GameStatus PlayAGame(IBoard board, List<Player> players);
    List<IPlayerScore> PlayerScores();
    string ToString();
    List<Player> Winner();
}

#endregion