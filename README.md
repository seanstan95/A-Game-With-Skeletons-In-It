# A Game With Skeletons In It
This was a game I made for a Game Design elective course in Fall 2017. It is a functional (slightly buggy due to rushed development) 
game with 3 levels, containing 2 enemy types, 2 boss versions of those enemy types, and a unique final boss.
## Enemies & Hazards
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
 There are currently references to removed gameplay elements that did not get added in the end, that I will eventually remove entirely.
 
 ## Removed Powerups
 Originally there was planned to be powerups that dropped from enemies a random; These powerups could be additional damage, faster fire 
 rate, health restoration, to list a few. This idea was scrapped in the end due to the game not being long enough to make such a variety 
 of powerups viable without making the game's difficulty significantly easier as a result. 
