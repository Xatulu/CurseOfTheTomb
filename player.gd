extends CharacterBody2D
class_name Player

@export var speed: float = 600.0

func _physics_process(delta: float) -> void:
	var direction: float = Input.get_axis("move_left","move_right")

	velocity.x = direction * speed
	move_and_slide()
