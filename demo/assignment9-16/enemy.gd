extends Sprite2D
#create enemny class
class_name enemy

#create variables for health
@export var health = 3
#variables for pathfinding
@onready var navigation= $NavigationAgent2D
#ready function 
func _ready():
	#get varibales
	#find player path 
	var player=get_tree().get_node("player")
#process function 
func _process(delta: float) -> void:
	#navigation
	#if the navigarion is not finishied
	if navigation.

#movement function 


#take damage function 
func take_damage():
	print("damage")
	#lower health
	health-=1
	#check if they died
	if health<=0:
		#play die function 
		die()

#die function 
func die():
	#take away processing
	#play animation
	#free queue 
	queue_free()
	

#attack function 
