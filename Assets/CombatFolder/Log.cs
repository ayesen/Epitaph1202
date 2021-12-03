// log

// 09/21/21
// xmovement
// xaiming
// !projectile is combined with straight since they are typically the same, the straight line one is actually a projectile
	// xdestroy spells
	// xlimit range
// !added aoe that allows the player to choose specific location to cast the spell in a certain range, might come in handy
	// xrestrict range

// 09/22/21
// xdamage
// xcontrol
// xrecovery

// 09/25/21
// xmaterial framework 1.0
	//! materials are prefabs
	//! each mat prefab can have multiple effect structs
	//! effect struct = effect the mat gives to the spell
	//! there can be only one material to be activated
	//! combination of materials functions as one new material

// 09/30/21
// adjustments
	// projectile doesn't use gravity for now
	
// features
	// xcollision detection
	// xprojectile
		// xhave place to insert effect codes
	// xaoe
		// xspawn a transparent cyllinder to indicate collider
		// xscale aoe indicator according to the size of the collider
		// xhave place to insert effect codes
		// xfix the bug where no collision is detected
	// xpie
		// xuse angle to check if hit
		// xchange pie range indicator accordingly
		// xfind place to insert effect codes
	// xtarget
		// xfind place to insert effect codes

// 10/03/21
// adjustments
	// xnew .gitignore
	// xget current mat in hit detection and call effect from effect manager based on the mat's effects
	// xdamage
		// xone time damage
		// xdot
	// xctrl
		// xwalkability
		// xatackability
		// xfoece move
	// xrecovery
		// xone time heal
		// xhot
	// xsupply
		// xdrop mats

// 10/09/21
// xcombination
	// xput combat into level
// foce move
	// ximpulse knock back
	// xknock back enemy
	// xknock back player

// 10/22/21
// xcombat refine
	// xwindup and backswing
	// xdrop gradually
		// xdrop meter
		// xui

// 10/23/21
// combat refine
	// xchange to windup state when moving and pressed atk
	// xcancel backswing if cast new spell
	// drop
		// xdrop debugging
		// xrefining
	// deal damage
		// xflash pie
		// xrefining
		// xfreeze frame
	// damage avoidence
		// gp
		// xknock back
	// effect
		// speed up (for dash juice G)
		// slow down
		// aoe dot and hot need to be effective only when in the aoe range
		// multiple ctrl type
// xobject inspection
	// xtext prompt
// xobjects in level solution
	// xenemy knocks back objects
	// xobjects disappear
// xalt cam
	// xperspective
	// xlook at enemy

// 10/31/21
// xvisual cue to indicate backswing state
// vfx
	// xprojectile
	// aoe
		// xheal
		// xdmg
		// dot
	// pie
	// xhot
	// dot

// 11/03/21
// xtry light on vfx
// xrefine vfx
	// xfragment on enemy and on other stuffs
// xdebug cancelling
// xenemy lock-on

// 11/05/21
// xdialogue
	// xtriggering
		// xauto
		// xpress e
	// xshow cht and eng
	// xshow image
	// xoptions
		// xdifferent following dialogues

// 11/07/21
// wall break
	// particle effect
		// xbricks
		// dust
	// xfunction
// xdialogue with ai

// 11/11/21
// xdynamic line duration adjustment according to clip length
// xwall hiding

// 11/13/21
// combat system revamp
	// store effects on mat
	// player decide what type of collider to spawn according to mat
	// effect processing
		// effect attachment
			// effects for enemy, (such as damage the enemy, stun the enemy, break the enemy, debuffs etc.), are added to the enemy when a hit detection collider hit the enemy
			// effects for player, (such as buffs), are added to the player when the player cast the spell
			// effects that are aoe, will spawn hit detection collider when the spell dies
		// condition process
			// when a condition is met, a condition struct will be made and the condition and the object that triggers the condition will be recorded and pass the the effect manager new
			// then the effect manager new will process each condition struct and call events based on different condtions
			// each event (such as dmg dealt event), will realize the effects it contains, based on the fired spell that is.
	// select a few effects to realize
		// situations
			// xwhen dmg dealt
			// xwhen collider hit
			// xwhen casting
		// effects
			// xdmg
			// buffs
			// xdebuffs
			// xstun
			// xbreak (drop)
			// heal
			// xknock back
			// xmultiple collision detection
			// xmultiple collider spawning
			// xtotem
				// xdifferent attack range
				// xadd another toWhom enum that doesn't record effects on objects, and just do shit when the spell projectile dies
				// xleave an aoe collider when the spell projectile dies
				// xpass effects to the collider
					// xcollider needs to be able to inflict effects to other game objects, record them onto objects, and let the effect manager new do the job
					// xcollider needs to destroy itself based on the forHowLong of the effect
		// mats
			// xaoe, dmg decay based on distance
			// xnormal spell, deal dmg and knock back
			// xheal when dmg dealt
				// xhealing
				// xadd modifer
			// xadd break dmg when dmg dealt / when break dealt / when hit / etc.
	// pick up mats
		// currently picking up mats will restore the mats to its starting amount

// 12/02/2021
// capstone 1 final
	// conversation with bear after bear's death
	// small bear mat
	// print mat effects
	// limit player selection