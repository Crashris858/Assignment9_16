extends Sprite2D


#variables
var TeleportAvail =true
signal TeleportPlayer (move_location: Vector2)

#process
func _process(delta: float) -> void:
	#move to global location 
	global_position=get_global_mouse_position()
	#move location
	var move_location=get_global_mouse_position()
	#on teleport 
	if Input.is_action_just_pressed("teleport") and TeleportAvail:
		#set teleport avaible to false
		TeleportAvail = false; 
		#start the timer 
		$teleport.start()
		#send a signal to player node
		TeleportPlayer.emit(move_location)
		
	

func _on_teleport_timeout() -> void:
	# set teleport to true
	TeleportAvail=true
	
