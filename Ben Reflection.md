## Ben King
# kingbenj
# Flexible programmer
# Animal roles not used  
  
Code involvement:  
  
SpeedPlatform – All  
CoinScript – All  
PauseMenu – All  
ScoreScript – All  
Player_Script – Some  
End_Level – Some  

In the Player_Script I added the code related to the speed platforms and the collectables. (Methods SpeedPaltform, GetScore, ChangeScore, ChangeVelocity and the associated variables.)

For End_Level I did not write any of the code but Adam used my pause menu script and adapted it for this script.

In addition to writing the code for the speed platforms and collectables I fully created two speed platform prefabs, one to increase speed and one to decrease speed, and two coins as collectables, one to increase the score and one to decrease score (this one is not used in any of the build levels). For these prefabs I did all aspects including visuals (hence the simple design), animation, and sounds.

For the pause menu, I, again, did all aspects except the background image, which I still had to modify as the design given to me did not fit with the menu layout I had already made.

All other work I did on this project was just small tweaks made in the inspector and some suggestions, most notably the suggestion to change most of the public variables to serialize fields to help with encapsulation, which I feel is a good habit to get into.

The most interesting part of my code, I feel, is the methods relating to the speed platforms in the player script (SpeedPaltform and ChangeVelocity). Here I had to integrate my code into code written by someone else, this code, while not written badly, was not how I would have written it, so I had to blend my code into the existing code. I feel it is also interesting because it was a situation in which I could use an optional argument, something not very common in C type languages, but something I like to use because of using R quite a bit.

The bit of code I’m most proud of is the same as above for the reasons talked about there too. The thing that I am overall most happy with is the pause menu although the code for it is simple overall it had many little parts which build it. I enjoyed the challenge of getting all these parts to work together and getting something that was both visually pleasing and functional.

During this project I have learnt a lot about Unity and C#, having never used either before this course. I have learnt that there are many different ways to approach a problem and other people come up with a solution that I would never have thought of.

The most important thing I will take away from this project is the knowledge I have gained from Unity which I am already starting to use as I have begun development on my own project.
