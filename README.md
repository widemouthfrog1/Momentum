# COMP313 Game Prototype

## Karl Bennett - bennetkarl

## Game Loop
The core game loop is move the player to traverse the level, solving puzzels based on the unique movement of the game and collecting coins that change the players score. 
At the end of each level you are notified of your final score and then are able to move on to the next level.


## Game Flow
In order to move forward the player must work out how they can progress given the moveset they have (of changing between circle and square, rolling with the circle (and with the square in the air), and piston jumping with the square). 
The player encounters diffirent obstacles that can change the players movement (speed platforms, lifts, etc.) or change the play environment (breakage walls). 
These obstacles add an extra challenge to the level as the player must learn to use them to their advantage.
These "puzzles" are designed to be easy to accomplish once you figure out what you need to do. Later levels are more difficult that the earlier ones.

## External Libraries/Assets
  
[Animated 2D Coins](https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097) - From the Unity store, modified to fit our purpose.  
[Universal Sound FX](https://assetstore.unity.com/packages/audio/sound-fx/universal-sound-fx-17256) - From the Unity store, small number of sounds from this pack used.

## How to play
Controls:

'A' to roll left.

'D' to roll right.

'S' to change between circle and square.

'Space' to extend pistons (jump) while you're a square (release to retract)

'Esc' or 'P' to pause the game

