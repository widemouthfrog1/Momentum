# Adam Xiong
# xiongadam
# Programing Lead
# Animal role not used

## Code Discussion:

CheckPoint_Script - Some
End Level screen - Most
I wrote the entire script, but I had based the methods off the PauseMenu script a group member had made
Kill_Script - Touched
I debugged the Kill method so that when the player spawned, it didn’t carry over the pistons momentum. Without this, the player will fly away from the checkpoint when respawned and possibly kill themselves. Lines 56-65
Piston_Script - Touched
I debugged the FixedUpdate method so that when the player switches from the circle to the square, the pistons will gain both the angular velocity and linear velocity of the player. Without this, if the player was flying in the air as the circle and then transformed to the square, the player will suddenly dive downwards Lines 76-77
TopOnlyPlatform - All
BezierCurverCollider2D - Some
I was the one who found the draft of the code online and then edited it to that it would work in our project

The most interesting piece of the code I wrote probably has to be the BoxBottom() method I wrote to calculate the y-coordinate of the bottom of the square relative to the centre. This is a part of the TopOnlyPlatform script, which is used to only allow a platform to activate it’s boxcollider2D when the player is above it. I wrote the script so that the collider activates when the bottom of the player is above the top of the platform. Calculating the bottom of the circle is easy, since that is a constant distance from the centre at all times. The bottom of the square is more difficult as the distance from it to the centre changes with the rotation. To calculate this, I had to brush up on trigonometry to develop a formula that would calculate the distance from the centre of the square to the bottom-most point at every rotation.

The code I am most proud of is probably the BoxBottom() method again. Having to calculate the bottom of the box would likely be a difficult task if you hadn’t kept up with mathematics since high school, but luckily I’m also a maths student so I was able to come up with a quick and elegant solution. But as a note for the future, I’m not sure a while loop was the best way to reduce the angle to less than 45 degrees, but it worked with no noticeable lag.

Learning Reflection:
I learned how to use smart commits/issues/etc in GitLab
The importance of creating branches for the purpose of debugging/retrieving deleted code
How to implement UI onto the canvas of a scene and write the necessary scripts to make Pause menus/Main menus/Buttons/etc
Cooperating with a group to build upon a concept and flesh it out into a finished product
The importance of good coding habits to make it easier for another person to read my code (the code Karl made for the prototype weren’t that great and he had to explain to us in person how it worked)

The most important thing I’ll take away from this project is the importance of making sure everyone in the group is on the same page and is aware of what the current priorities are at all times. There were moments during development where some of us were not sure what we could be doing, while others working on several different scripts by themselves.
