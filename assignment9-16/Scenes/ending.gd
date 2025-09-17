extends CanvasLayer


#variable for clicks
var clicks=0

#input functions\
func _input(event: InputEvent) -> void:
	#if event is click
	if event is InputEventMouseButton and event.is_pressed():
		#hide all sppech
		$ColorRect/ColorRect/speech1.hide()
		#increment clicks
		clicks+=1
		get_tree().change_scene_to_file("res://Scenes/main_menu.tscn")
