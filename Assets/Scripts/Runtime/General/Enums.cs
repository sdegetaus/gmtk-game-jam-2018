﻿public enum Scene
{
    LoadingScreen = 0,
    Main = 1
}

public enum PoolTag
{
    ObstacleGroup,
    CollectableGroup,
    Arrows,
}

public enum Lane
{
    Left = -1,
    Middle = 0,
    Right = 1
}

public enum ObstacleEnum
{
    SingleNormal,
    DoubleNormal,
    SingleSpecial_0,
    DoubleSpecial_0,
    DoubleSpecial_1,
    DoubleSpecial_2
}

public enum CollectableEnum
{
    CollectibleTest0,
    CollectibleTest1
}

public enum UIState
{
    MainMenu,
    Settings,
    InGame,
    Pause,
    RunOver,
    Empty,
}

public enum ChangeGUIStateMode
{
    Single,
    Additive
}