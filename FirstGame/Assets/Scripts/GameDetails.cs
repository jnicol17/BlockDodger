using System;

[Serializable]
public class GameDetails{

    public int highscore;

    public GameDetails()
    {
        this.highscore = 0;
    }

    public int getHighScore()
    {
        return this.highscore;
    }

    public void setHighScore(int newHighScore)
    {
        this.highscore = newHighScore;
    }
}
