class_name hurtbox
extends Area2D

#create a signal for recievng damage
signal recieved_damage(damage:int)

@export var health = 1

#if entered by an area 
