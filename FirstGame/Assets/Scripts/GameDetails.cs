using System;

// this code is the GameDetails that are stored in persistant data
// currently only the highscore is saved, soon game settings will be included

[Serializable]
public class GameDetails{

    public int highscore;

    public GameDetails()
    {
        this.highscore = 0;
    }
}
