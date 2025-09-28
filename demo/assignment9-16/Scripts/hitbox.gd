class_name hitbox
extends Area2D

#create variable for damage
@export var damage = 1

#set damage
func setDamage (value: int):
	value=damage
	
#get damage 
func getDamage () ->int:
	return damage
