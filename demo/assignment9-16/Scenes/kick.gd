extends Kick 

#variable stop
var stop=false; 

#conncet on_player_kick singanal
@onready var playerPath: NodePath = ("../player")

func _ready():
	#set process
	set_process(true)
	#get node 
	var player_node = get_node(playerPath)
	player_node.connect("kick_object", Callable(self, "_on_player_kick_object"))
	
func _on_player_kick_object(direction: Vector2) -> void:
	print("debugKickNode")
	#call kicked
	kicked(direction, kickForce)
	#set stop to false
	stop=false; 
func _process(delta: float) -> void:
	#get velocity 
	var vel= get_velocity()
	#handle animations
	if !stop:
		animation_spin(vel.length());
	else:
		#timeout stop 
		await get_tree().create_timer(0.5).timeout
		#start animaion 
		animation_stop(stop);
	#if moving
	if vel.length() > 0:
		#update position
		position +=vel* delta
		#apply a frcition
		set_velocity(vel *0.99)
		
func _on_area_2d_body_entered(body: Node2D) -> void:
	#if body is in group enemy 
	if body.is_in_group("enemy"):
		#emit enemy collid signal
		emit_signal("object_hit", body, damage);
		#stop animation 
		animation_stop(stop)
	#if body is in group in phsyics layer 0 tile map
	elif body is TileMap:
		var direction=(global_position-body.global_position).normalized()
		#move to prevent colliding.
		global_position.x+=direction.x*20
		#run reverse
		reverse_direction(direction)
