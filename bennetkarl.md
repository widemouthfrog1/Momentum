# Karl Bennett - bennetkarl
### Lead

## My Contribution

### All
* Kill_Script.cs
    - This file has not changed since the prototype. It would probably be better to have a kill/move function on the player that gets called with the checkpoint position as a parameter but it worked so it wasn't a high priority.
* Level_Select.cs, Options.cs, and MenuScript.cs
    - These files are all very similar, just changing scenes with the scene manager. I didn't get around to creating a proper options menu as it also was not a high priority.
* Piston_Script.cs
    - I tried applying the change to try and fix the bug highlighted during the prototype but the pistons still fly out sometimes. It does happen less frequently however. I also added some code to remove the angular velocity of the pistons when changing from circle to square.
* Wall_Segment_Script.cs
    - This script makes the segments of the breakable wall no longer interactive with the player once they fall outside of the bounds of the original wall. It also draws a debug-line in the scene view (when the game is running) that shows the bounds of the box where the segments can be interacted with.

### Most
* Checkpoint_Script.cs
    - This script has remained mostly untouched since the prototype, however another developer changed the checkpoints to only be able to be activated once.
* Player_Script.cs
    - My main contribution to this script was when creating the prototype. Other changes I made were bug fixing and making the player lose all angular velocity when changing from circle to square.
* BezierCurveCollider2D.cs
    - The majority of the script we found at [this website](https://stackoverflow.com/questions/25958171/how-can-i-create-a-2d-curve-collider) which another developer copied and imported into out project. I made the UI easier to use by making the control points get smaller as you zoom in (and bigger as you zoom out) and added the ability to hide the UI with a toggle button. In hindsight it would have worked better with the UI toggled off to begin with as the UI re-appeared whenever you reselected the object the curve was on.

### Some
* Wall.cs
    - I helped peer program this file with Devon. This script generates a breakable wall by linking square game objects with fixed joints. The parts I did alone was linking the Wall_Segment_Script to each segment and fine-tuning the parameters of the wall.

### Touched
* End_Level.cs
    - Changed how LoadNextLevel() worked so that it used level indices