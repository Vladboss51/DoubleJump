extends Node2D

func _on_go_pressed() -> void:
	get_tree().change_scene_to_file("res://lvl_1.tscn")