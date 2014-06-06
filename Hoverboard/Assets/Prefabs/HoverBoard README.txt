HoverBoard 3.3. DEFAULT VALUES & DOCUMENTATION

RigidBody

	Mass = 100;
	Drag = 4;
	Angular Drag = 1;
	Use gravity = true;

Hover_Physics
	
	Landing Power = 5;	// The force which pulls the hoverboard in local down.
	Jumping Power = 5;	// The force whichs pushes the HoverBoard in local up.
	HoverHeight = 5;	// The hovering height of the HoverBoard.

Movement

	Boost Max Acc Speed = 90;	// The maximum speed the hoverboard can gain with boost, reqiured to be higher than Max Acc Speed.
	Boost Acceleration = 1;		// Boost Acceleration.
	Max Jump Power = 100; 		// Max Jump Power.
	Gravity = 0.3;			// Gravity acceleration, added each frame when not grounded.
	Friction = 0.2;			// SpeedLoss, every frame.
	
	Max Acc Speed = 40;		// The maximum speed that can be gained from accelerating.
	Forward Acc = 1;		// Acceleration in forward Direction.
	Backward Acc = 1;		// Acceleration in BackWard Direction.
	Rotate In Sec = 0.5;		// After leaving ground, The hoverboard can start rotating in x seconds.
	
	Angle Speed = 1;		// Multiplier, how fast the hoverboard should rotate to a new angle.
	Max Angle = 50;			// the absolout max angle the hoverboard can obtain.
	Snap Angle = false;		// Snap to a angle instead of lerping.
	Snap At Height = 0;		// Snap when the Hoverboard reaches a certain height from the ground (Check HoverHeight).
	
	Potential Speed = 1;		// Multiplier, Speed gained from going downhill/uphill, separated from normal Speed.
	Potential Friction = 0.5;	// Friction loss on going downhill/uphill, separated from normal Friction.

Energy Pool
	
	Energy = 0; 			// Energy pool.
	Start Energy = 0;		// Start Energy.
	Max Energy = 10;		// Maximum energy that can be obtained.
	Energy Regen Rate = 1;		// Energy Regenerate every Second.
	
Boost
	
	Is Boosting = true/false;	// are you boosting? boost with B-key.
	Energy Drain Speed = 1;		// and stuff
	Pause Duration = 1;		// stuff


	
	