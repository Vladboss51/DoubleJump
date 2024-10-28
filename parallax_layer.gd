extends ParallaxLayer

var speed = 20

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	motion_offset.x -= speed * delta
