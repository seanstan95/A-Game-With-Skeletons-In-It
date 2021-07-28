# A Game With Skeletons In It
This is a game I made for a Game Design elective course in Fall 2017. It was meant to be a group project, but as the commit history shows, I did the vast majority of the work.
It is a functional simple 3D Doom Clone-esque game with 3 levels and 5 enemy types in the form of 3 bosses and 2 enemies.

The project has been updated as of July 2021 to work with the most recent version of Unity (2020.3.14f1) and I've enjoyed scrubbing over the scripts over time to find various tiny optimizations and logic simplifications.
Cloning the project should be all you need to open and edit the project in Unity, and you can play the most recent built version by looking at the Releases section on the right.

## Enemies
**Skeletons** - Simple melee enemies that are taken from a free asset Skeleton model. Animated to chase after the player and attack when in range.

**Wizards** - Simple ranged enemies that are taken from a free asset Wizard model. Animated to float in place and shoot at the player with placeholder square projectiles due to time constraints of getting fully working particle projectiles in.

**Skeleton Boss** - Larger version of the standard Skeletons with more health and attack power. Appears at the end of level 1 and a rematch in level 3.
 
**Wizard Boss** - Larger version of the Wizard enemies, with a new feature. Boss apepars in one of 6 "rooms" and regularly teleports to other rooms to make the player chase it down. Appears at the end of level 2 and a rematch in level 3.
 
**Final Boss** - Unique boss faced at the end of level 3. Boss has a shield that can not be penetrated until 1 of 5 targets in the room is shot at, at which point the boss grows in size and chases the enemy until 1/5 of its health is gone.
 
## Scripts
I spent a lot of time making sure the scripts are as condensed/simplified as possible throughout the game's development. 
In the early stage of planning, there was meant to be a Powerup system where enemies would drop powerups that would change the player's fire rate, damage, etc. When implementing this, it became clear that powerups made the game too easy (on top of already being an easy game). The powerup system was functioning before removal. The associated scripts have been moved to Scripts/Unused, and references to them that existed in other scripts have been removed.