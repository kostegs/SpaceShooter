using System;

public class LevelPointEventArgs : EventArgs
{
    public int _spawnPointNumber;

    public LevelPointEventArgs(int spawnPointNumber) => _spawnPointNumber = spawnPointNumber;
}
