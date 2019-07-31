# Blink

Simple 2D project made in UnityEngine


The name "Blink" comes from the sound that makes the teleport spell.

The game consists of a sorcerer's duel, in which both sorcerers must kill each other.
Each sorcerer can shoot fireballs to damage his opponent.
In addition, they can teleport over short distances to dodge enemy attacks.

There are 2 types of PowerUps: 
  -Blue: Multiplies x3 the mana regeneration rate and reduces the cooldown of the fireball spell.
  -Red : Heals the sorcerer;
  
The enemy has an simple AI. He will chase down the player and try to kill him, while dodging his attacks. If his health falls below %50,
he will run quickly to a Red PowerUp. But first, he must know the localization of it. In order to know this, the enemy has a detector that 
saves the PowerUps location for later use.

