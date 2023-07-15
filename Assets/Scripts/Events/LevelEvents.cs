using System.Collections.Generic;
using System;

public static class LevelEvents
{
    public delegate void NextLevelParam();
    public static event NextLevelParam OnNextLevelParam;

    public static void InvokeNextLevelParam()
    {
        OnNextLevelParam?.Invoke();
    }
    public delegate void NewLevelEvent(LevelParameters levelParameter);
    public static event NewLevelEvent OnLevelParamChanged; 
    public static void InvokeLevelParamChanged(LevelParameters currentLevelParameters)
    {
        OnLevelParamChanged?.Invoke(currentLevelParameters);
    }
}