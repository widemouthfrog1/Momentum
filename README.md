# COMP313 Game Prototype

## Karl Bennett - bennetkarl

## Game Loop
The core game loop is move the player to traverse the level, solving puzzels based on the unique movement of the game. You will then be able to unlock the next level however in this prototype this is not yet implemented.
As an extra touch, I added a timer that times how long it took you to complete the level. I plan on adding a leaderboard so you can speedrun against your friends.

## Game Flow
In order to move forward the player must work out how they con progress given the moveset they have (of changing between circle and square, rolling with the circle (and with the square in the air), and piston jumping with the square). These "puzzles" are designed to be easy to accomplish once you figure out what you need to do. Later levels could use puzzels from earlier levels conjunctively (two or more puzzels together)

## External Libraries/Assets
No external libraries or assets were used

## How to play
Controls:\n
a to roll left.

d to roll right.

s to change between circle and square.

space to extend pistons while you're a square (release to retract)

That's pretty much it

## Challenging/Interestng Parts
The slider joints required to make the pistons extend don't stay out very well on their own, they bonce around like weak springs. To fix this, I added a fixed joint to the piston that gets activated whenever the piston is fully retracted or extended and gets deactivated on button press/release. I made a finite state machine to get my head around it.

Another challenge I had was that originally we had small pistons that you could see the end of when they extended and we changed them to be bigger. Once they were bigger the center of mass was further out and so the circle's speed was significantly decreased. The fix I implemented was to change the center of mass of the pistons to be the center of the circle while you're the circle. It worked but I'm not sure if I managed to put the center of mass for all the pistons in the right place as there's no visual indication. I may try decreasing the mass of the pistons while you're the circle instead.

A challenge that's yet to be fully tackled is that it's difficult to know where you're going as you can't see very far. The solution we have at the moment is a bunch of arrow/signs showing you the way. A better solution might be a minimap that shows you more of the level but with lower detail so that you can see where you're going and what your objective is.