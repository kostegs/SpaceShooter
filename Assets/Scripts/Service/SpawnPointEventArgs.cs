using System;

public class SpawnPointEventArgs : EventArgs
{
    public int _spawnPointNumber;

    public SpawnPointEventArgs(int spawnPointNumber) => _spawnPointNumber = spawnPointNumber;
}
