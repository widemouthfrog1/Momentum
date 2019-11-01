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
* BezierCurveCollider2D.cs and BezierCurveCollider2D_Editor.cs
    - The majority of the script we found at [this website](https://stackoverflow.com/questions/25958171/how-can-i-create-a-2d-curve-collider) which another developer copied and imported into out project. I made the UI easier to use by making the control points get smaller as you zoom in (and bigger as you zoom out) and added the ability to hide the UI with a toggle button. In hindsight it would have worked better with the UI toggled off to begin with as the UI re-appeared whenever you reselected the object the curve was on. I also fixed the colliders being misplaced under some circumstances.

### Some
* Wall.cs
    - I helped peer program this file with Devon. This script generates a breakable wall by linking square game objects with fixed joints. The parts I did alone was linking the Wall_Segment_Script to each segment and fine-tuning the parameters of the wall.

### Touched
* End_Level.cs
    - Changed how LoadNextLevel() worked so that it used level indices

[Most Interesting Code](https://gitlab.ecs.vuw.ac.nz/comp313-2019-a3/t9/comp313_game_prototype/blob/master/Assets/Scripts/Player_Script.cs#L105-124)\
I found this interesting because the most intuitive solution to resetting the rotation of objects is to reset them to zero. However here, the original rotation of the pistons are all different depending on which direction they're facing. This problem plagued me for a bit and I'm proud that I managed to figure out what was going wrong (the observable behaviour I had to work with was the square tilting to the right a bit when changing from circle to square instead of being flat)

[Code I am most proud of](https://gitlab.ecs.vuw.ac.nz/comp313-2019-a3/t9/comp313_game_prototype/blob/master/Assets/Scripts/Custom%202D%20Colliders/Scripts/Editor/BezierCurveCollider2D_Editor.cs#L79-104)\
This code I am most proud of because I had never touched Unity GUI before and was working with someone elses code, and I still managed to both understand Unity GUI and that code enough to make the changes I wanted to.

## What I've Learnt
* How to and why you should use GitHub/GitLab issues (and labels).
* What it's like to be a team lead.
* The basics of scripting in Unity, from loading scenes to the difference between FixedUpdate and Update (always put control input handling in Update).
* Reverting commits with Gitlab and GitHubDesktop.
* How to screen capture with sound on Windows (I made the feature demonstration video)
I have also come to appreciate how planning before coding can decrease the amount of time needed to code something and the frustration caused by it (when you get something wrong because you don't know what you're doing). Also peer programming works. It makes coding much less stressful when mistakes can be found when the code is written instead of spending hours later trying to find the place where you went wrong.

## Most Important Thing I Can Take From This Project
Communication in a project is important. Making sure everyone is on the right page can mean the difference between making progress and spending hours doing something that was either slightly wrong or wasn't needed. I think our team was fairly good at this on the whole but there were a few times when people had completely different interpretations of what was wanted.
