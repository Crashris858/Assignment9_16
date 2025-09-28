extends CharacterBody2D

#create variables for health
@export var health = 3
@export var speed = 200
#variables for pathfinding
@onready var navigation= $NavigationAgent2D
@onready var animated: AnimatedSprite2D = $AnimatedSprite2D
@onready var playerPath: NodePath = ("../player")
var player: CharacterBody2D
var dead = false
#create signals
signal attackPlayer
signal deathCounter
#stateflags
var attacking =false
#ready function 
func _ready():
	#get varibales
	player=get_node(playerPath)
	#find player path 
	if player:
		navigation.set_target_position(player.global_position)
	
#process function 
func _process(delta: float) -> void:
	if player:
	#navigation
		navigation.set_target_position(player.global_position)
		#face correct position
		look_at(player.global_position)
		#if navigation is not reachable
		if !navigation.is_target_reachable():
			#play idle
			animated.play("idle")
		#if the navigarion is not finishied
		elif !navigation.is_navigation_finished() and !attacking:
			#play walking animation 
			animated.play("walk")
			#find the direction that it will go
			var direction=(navigation.get_next_path_position()-global_position).normalized()
			#velocity
			velocity=direction*speed
			#move
			move_and_slide()
		#attack
		#check if they are near target 
		if navigation.distance_to_target()<100 and !attacking:
			#play attack function
			attack()


#take damage function 
func take_damage():
	print("damage")
	#lower health
	health-=1
	#emit to hud 
	#check if they died
	if health<=0 and !dead:
		#play die function 
		die()

#die function 
func die():
	#set die to true
	dead=true
	#send death counter signal
	deathCounter.emit()
	#take process away 
	set_process(false)
	#play animation
	$AnimatedSprite2D.play("die")
	#disable the hurtbox
	$CollisionShape2D.disabled=true
	#timer 
	await get_tree().create_timer(0.25).timeout
	#free queue 
	queue_free()
	

#attack function 
func attack():
	#set attack to true
	attacking=true
	#enable area 2d and monitoring
	await get_tree().create_timer(1).timeout
	#play attack 
	animated.play("attack")
	$punch.monitoring=true
	$punch/CollisionShape2D.disabled=false
	#set a timer
	await get_tree().create_timer(0.25).timeout
	#disable 
	attacking=false
	$punch.monitoring=false
	$punch/CollisionShape2D.disabled=true
	animated.play("idle")


func _on_punch_body_entered(body: Node2D) -> void:
	#check if body is player
	if body is Player:
		body.take_damage()
		


func _on_kick_object_hit(Target: Object, damage: int) -> void:
	# if it self
	if Target==self and !dead:
		#run die function 
		die();
