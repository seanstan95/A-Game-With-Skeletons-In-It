# A Game With Skeletons In It
This was a game I made for a Game Design elective course in Fall 2017. It is a functional (slightly buggy due to rushed development) 
game with 3 levels, containing 2 enemy types, 2 boss versions of those enemy types, and a unique final boss.
## Enemies
**Skeletons** - Simple melee enemies that are taken from a free asset Skeleton model. Animated to chase after the player and attack 
when in range.

**Wizards** - Simple ranged enemies that are taken from a free asset Wizard model. Animated to float in place and shoot at the player 
with placeholder square projectiles due to time constraints of getting fully working particle projectiles in.

**Skeleton Boss** - Larger version of the standard Skeletons with more health and attack power. Appears at the end of level 1 and a rematch
 in level 3.
 
 **Wizard Boss** - Larger version of the Wizard enemies, with a new feature. Boss apepars in one of 6 "rooms" and regularly teleports to 
 other rooms to make the player chase it down. Appears at the end of level 2 and a rematch in level 3.
 
 **Final Boss** - Unique boss faced at the end of level 3. Boss has a shield that can not be penetrated until 1 of 5 targets in the room 
 is shot at, at which point the boss grows in size and chases the enemy until 1/5 of its health is gone.
 
 ## Scripts
 I spent a lot of time making sure the keep the scripts as condensed/simplified as possible throughout the game's development. 
 In the early stage of planning, there was meant to be a Powerup system where enemies would drop powerups that would change the player's fire rate, damage, etc. When implementing this, it became clear that powerups made the game too easy (on top of already being an easy game). The powerup system was functioning before removal. The associated scripts have been moved to Scripts/Unused, and references to them that existed in other scripts have been removed.
