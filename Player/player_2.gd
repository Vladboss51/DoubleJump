extends CharacterBody2D

var k = 0
const SPEED = 250.0
const JUMP_VELOCITY = -400.0

@onready var anim = $AnimatedSprite2D

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("jump2") and is_on_floor():
		velocity.y = JUMP_VELOCITY
		anim.play("jump")
		k = 10

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("ui_left", "ui_right")
	if direction:
		velocity.x = direction * SPEED
		if velocity.y == 0:
			anim.play("run")
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		if velocity.y == 0:
			anim.play("idle")

	if direction == -1:
		$AnimatedSprite2D.flip_h = true
	elif direction == 1:
		$AnimatedSprite2D.flip_h = false
	
	if k > 0 and velocity.y == 0:
		anim.play("land") 
		k -= 1
	
	move_and_slide()
