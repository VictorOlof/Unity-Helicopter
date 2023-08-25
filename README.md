# Unity-Helicopter-Game
Helicopter game made for mobile

### LevelEvents
- OnSetPlayerCKPT
 
  Invokes by LineManager when OnLevelTimerComplete-event occur.

- OnPrepNewLevelEvent (LevelParameters levelParameters)

  Invokes by LevelManager when OnSetPlayerCKPT-event occur.
  Sends LevelParameters for the upcoming level.

- OnPlayerCKPT

  Invokes by PlayerMovement when player runs into checkpoint (the new level).
  
- OnNewLevel (LevelParameters levelParameters)

  Invokes by LevelManager when OnPlayerCKPT-event occur.
  Sends LevelParameters for the current level.

  LevelManager starts a new timer.
 
- OnLevelTimerComplete 

  Invokes by LevelTimer when timer is finished.


