# Assignment9_25
This assignment was about creating a GDExtension. I created a kickable node that extended Sprite 2D. It has methods to be kicked, spin around, and bounce off of walls. The source files are in the src folder, and the assignment and the GDextension file are in the demo file. I created a scene called Kick using the node and put that in the main scene to demonstrate functionality. There are also functions to spin the node when it's speed increases as well as to stop it for certain scenarios. Everything is binded the same except for process and ready, since those are written in the engine and it would not be favorable if they were overwritten. In the script, I added it's position update, as well as a singal it receives when the player touches it. Furthermore, there is one more singal it emits when touching an enemy to kill it.

# Controls 
Move - WASD

Mouse - Aim 

Left Click - Attack/Progress text

Right Click - Teleport
