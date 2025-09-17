extends CanvasLayer

#on ready

#press play button 
func _on_play_pressed() -> void:
	#load the new scene
	get_tree().change_scene_to_file("res://Scenes/directions.tscn")
	


func _on_quit_pressed() -> void:
	get_tree().quit()


func _on_ready() -> void:
	$Camera2D.make_current()
