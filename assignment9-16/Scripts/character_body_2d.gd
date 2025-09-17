extends CharacterBody2D

#class name
class_name Player
#movement variables
@export var Speed=600
@export var health=3
var punchTimer = 0

#signals
signal healthUpdate
signal gameOver 

#stateflags
var Teleporting=false
var punching=false
var invincible=false
@onready var animated: AnimatedSprite2D = $AnimatedSprite2D

#screen size
var screenSize =get_viewport_rect().size

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

	
	
#set a seperate  procees function for the animation 
func _physics_process(delta: float) -> void:
	#flags
	if Teleporting:
		return
	look_at(get_global_mouse_position())
	#get the vector between all and mutiply be speed
	velocity = (Input.get_vector("move_left","move_right","move_up","move_down"))*Speed
	#check for movemnet animation 
	#stateflags
	if !punching:
		if velocity.length()>0:
			$AnimatedSprite2D.play("walk")
		else:
			$AnimatedSprite2D.play("idle")
	#use move and slide to move
	move_and_slide()
	#punch and state flag not active 
	if Input.is_action_pressed("action") and !punching:
		#run punch
		punch()
	#if punching
	if punching:
		#increment punch timer
		punchTimer+=delta
		
	#on punch relase
	if Input.is_action_just_released("action") or punchTimer>=0.10:
		#set punch to false
		punching=false
		#disable the collison
		$attack/punch.disabled=true
		#turn monitoring off
		$attack.monitoring=false
		#reset punch timer
		if punchTimer>=0.10:
			#enbale punch again
			punch()
		#reset punch timer
		punchTimer=0

#punching function 
func punch() -> void: 
	#set puncjing to true
	punching=true
	#punch enabled
	$attack/punch.disabled=false
	#set monitoring to true
	$attack.monitoring=true
	#play punch animation 
	$AnimatedSprite2D.play("punch")
	
	
	
func _on_reticle_teleport_player(move_position: Vector2) -> void:
	#Set state flag
	Teleporting=true
	#play teleport animation 
	$AnimatedSprite2D.play("teleport")
	#take away input
	set_process_input(false)
	#await end
	await get_tree().create_timer(0.5).timeout
	#move player
	global_position=move_position
	#restore input
	set_process_input(true)
	#reset state flag
	Teleporting=false
	

func _on_attack_body_entered(body: Node2D) -> void:
	print("d")
	#check if the pulled body is an enemy
	if body.is_in_group("enemy"):
		#play body's damage method
		body.take_damage()
		
func take_damage():
	#if not invincible
	if !invincible:
		#subtract health
		health-=1
		#if health is zero emit game over
		if health<=0:
			#set process to false
			set_process_input(false)
			set_process(false)
			#emit game overr singal 
			gameOver.emit()
		#emit signal 
		healthUpdate.emit(health)
		#set health to invincible 
		invincible=true
		#start timer
		$invincibleTimer.start()


func _on_invincible_timer_timeout() -> void:
	#change back to false
	invincible=false
