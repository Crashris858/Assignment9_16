//header file for Kickable object
//inclusion guard 
#ifndef Kick_H
#define Kick_H

//extend Sprite 2D
#include <godot_cpp/classes/sprite2d.hpp>
//extend area2d
#include <godot_cpp/classes/area2d.hpp>
//extend vector2d
#include <godot_cpp/variant/vector2.hpp>
//MATHS
#include <godot_cpp/variant/utility_functions.hpp>

//using Godot Namespace 
namespace godot {

//Creating a new class that extends Sprite2D
class Kick  : public Sprite2D 
{
	//Godot Macro to create a new Node
	GDCLASS(Kick , Sprite2D)

private:

	//class variables
	Vector2 velocity;
	int damage; 
	int kickForce; 
	bool stop=false;
	// Vector2 position; 

protected:

	//used to bind methods. 
	static void _bind_methods();

public:

	//process and redy
	void _process(double delta) override;
	void _ready() override; 

	//constructor and Destructor 
	Kick ();
	~Kick ();
    
	//getter and setter
	void set_kickForce(const int kickForce); 
	int get_kickForce() const; 
	void set_damage(const int attack); 
	int get_damage() const; 
	Vector2 get_velocity() const;
	void set_velocity(Vector2 v);
	// void set_position(Vector2 p);
	// Vector2 get_position() const;

	//other functoins
	///kicked
	void kicked (Vector2 direction, int kickForce);
	//on 2d area (player or enemy entered)
	void on_hitbox_entered(Node* current);

	//animation functions 
	//spin 
	void animation_spin(float velocity);
	//spin stop 
	void animation_stop(bool stop);
	//reverse
	void reverse_direction(Vector2 Velocity);
};

}

//end defitnition
#endif