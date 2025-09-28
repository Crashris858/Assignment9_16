extends CanvasLayer


#variable for clicks
var clicks=0

#input functions\
func _input(event: InputEvent) -> void:
	#if event is click
	if event is InputEventMouseButton and event.is_pressed():
		#hide all sppech
		$ColorRect/ColorRect/speech1.hide()
		$ColorRect/ColorRect/speech2.hide()
		$ColorRect/ColorRect/speech3.hide()
		#increment clicks
		clicks+=1
		#if clicks =4 go to main scene
		if clicks==4:
			get_tree().change_scene_to_file("res://Scenes/main.tscn")
		#show proper speech
		var currentBox= "ColorRect/ColorRect/speech"+str(clicks)
		currentBox=get_node(currentBox)
		if currentBox:
			#show curretn box
			currentBox.show()
		
