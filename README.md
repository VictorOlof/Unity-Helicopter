# Unity-Helicopter-Game
Helicopter game made for mobile

### LevelEvents
- OnSetPlayerCKPT
 
  Invokes by LineManager when timer is finished.

- OnPrepNewLevelEvent (LevelParameters levelParameters)

  Invokes by LevelManager when checkpoint is set.
  Sends LevelParameters for the upcoming level.

- OnPlayerCKPT

  Invokes by PlayerMovement when player runs into checkpoint (the new level).
  
- OnNewLevel (LevelParameters levelParameters)

  Invokes by LevelManager when event above occur.
  Sends LevelParameters for the current level.
 
- OnLevelTimerComplete 

  Invokes by LevelTimer when timer is finished.


