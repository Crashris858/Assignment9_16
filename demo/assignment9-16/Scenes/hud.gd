extends CanvasLayer

#onready variables
@onready var enemiesKilled = $enemiesKilled
#update player health
func _on_player_health_update(health:int) -> void:
	$healthbar.play(str(health))


func _on_main_update_score(time_left: int) -> void:
	#update the score
	$score.text=str(time_left)


func _on_main_cow_boy() -> void:
	#change the labels 
	$Textbox/text1.hide()
	$Textbox/text2.show()
