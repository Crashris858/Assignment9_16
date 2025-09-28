extends Node2D

#variables
#signals
signal update_score (time_left:int)
signal cowBoy
#exported 
@export var time_left_counter = 20
@export var enemies: PackedScene
@export var enemyGoal= 10
#nodes
@onready var player: Player = $player
@onready var tile_map: TileMap = $TileMap
@onready var time_left: Timer = $timeLeft
@onready var hud: CanvasLayer = $HUD
@onready var kick: Kick = $kick


#counter 
var enemiesKilled=0


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#start the timer
	$timeLeft.start()
	
#game over function
func _on_player_game_over() -> void:
	#take away process
	set_process_input(false)
	#player death animation
	player.animated.play("die")
	#hide the hud
	hud.hide()
	#stop timers
	time_left.stop()
	$enemyTimer.stop()
	#wait 2 seconds
	await get_tree().create_timer(3.0).timeout
	#transition to game over
	get_tree().change_scene_to_file("res://Scenes/gameover.tscn")

func _on_time_left_timeout() -> void:
	#subtract from time left
	time_left_counter-=1
	#send signal
	update_score.emit(time_left_counter)
	#if no time is left 
	if time_left_counter<=0:
		#if enemy counter is above thresholddd
		if enemiesKilled>=enemyGoal:
			#enabled win
			get_tree().change_scene_to_file("res://Scenes/ending.tscn")
		#else gameover
		else:
			_on_player_game_over()
	#start timer again
	time_left.start()
		
func _on_enemy_timer_timeout() -> void:
	#create an array of vector 2 for spawn postition 
	var SpawnPoints=[]
	#istantiate enemy
	var newEnemy= enemies.instantiate()
	#check for available tiles
	for tile in tile_map.get_used_cells(0):
		#get tile data
		var SpawnData=tile_map.get_cell_tile_data(0,tile)
		#check if the data is the correct type
		if SpawnData and SpawnData.get_custom_data("enemySpawn"):
			#get into local coordinate system
			var coord=tile_map.map_to_local(tile)
			#check if it is in the current screen
			#if player.get_global_position()-get_viewport_rectangle()
			#append to array
			SpawnPoints.append(coord)
	#choose a random point from the array
	newEnemy.position=SpawnPoints[randi_range(0,SpawnPoints.size()-1)]
	#connect signal
	newEnemy.deathCounter.connect(_on_enemies_death_counter)
	kick.connect("object_hit", Callable(newEnemy, "_on_kick_object_hit"))
	#add to group enemy
	newEnemy.add_to_group("enemy")
	#add child 
	add_child(newEnemy)
	#restart timer
	$enemyTimer.start()
	


func _on_enemies_death_counter() -> void:
	print("reached")
	#increment enemies killed 
	enemiesKilled+=1
	#increment hud
	hud.enemiesKilled.text=str(enemiesKilled)
	#if enemies killed is 20 send cowboy signal
	if enemiesKilled==20:
		cowBoy.emit()
		
	
