//inclusions and using nameSpace 
#include "Kick.h"
#include <godot_cpp/core/class_db.hpp>


using namespace godot;


//bind methogs 
void Kick ::_bind_methods() 
{
    //binding
    //animation spin 
    ClassDB::bind_method(D_METHOD("animation_spin", "velocity"), &Kick::animation_spin);
    //animation stop
    ClassDB::bind_method(D_METHOD("animation_stop", "stop"), &Kick::animation_stop);
    //bindy ready 
    BIND_VIRTUAL_METHOD(Kick, _ready, 2139827523);
    //bind reverse 
    ClassDB::bind_method(D_METHOD("reverse_direction", "Velocity"), &Kick::reverse_direction);
    //bind process as virtual so scripts can override it
    BIND_VIRTUAL_METHOD(Kick, _process, 2139827523);
    //kick function 
    ClassDB ::bind_method(D_METHOD("kicked", "direction", "kickForce"), &Kick::kicked);
    //getter setter 
    ClassDB ::bind_method(D_METHOD("get_kickForce"), &Kick ::get_kickForce);
    ClassDB ::bind_method(D_METHOD("set_kickForce", "value"), &Kick ::set_kickForce);
    ADD_PROPERTY(PropertyInfo(Variant::INT,"kickForce"),"set_kickForce", "get_kickForce");
    //damage
    ClassDB ::bind_method(D_METHOD("get_damage"), &Kick ::get_damage);
    ClassDB ::bind_method(D_METHOD("set_damage", "value"), &Kick ::set_damage);
    ADD_PROPERTY(PropertyInfo(Variant::INT,"damage"),"set_damage", "get_damage");
    //velocity 
    ClassDB ::bind_method(D_METHOD("get_velocity"), &Kick ::get_velocity);
    ClassDB ::bind_method(D_METHOD("set_velocity", "v"), &Kick ::set_velocity);
    ADD_PROPERTY(PropertyInfo(Variant::VECTOR2,"velocity"),"set_velocity", "get_velocity");



    //add hit detected singal (name, target, damage)
    ADD_SIGNAL(MethodInfo("object_hit", PropertyInfo(Variant::OBJECT, "Target"), PropertyInfo(Variant::INT, "damage")));

}

//hit hurt variables (constructor)
Kick :: Kick ()
{
    //variables
    velocity=Vector2(0,0);
    //stop;
    stop=false; 
    //set base values
    damage=1;
    kickForce=300;
}
//deconstructor
Kick :: ~Kick ()
{
}
//ready 
void Kick ::_ready()
{
    //define in script
}
// other functions
//process 
void Kick::_process(double delta)
{
    //define in script 
}

//kicked
//called by signal 
void Kick :: kicked (Vector2 direction, int kickForce)
{
    //send in the direction of player at velocity
    //kickForce
    velocity=(direction.normalized()*kickForce);
}

//on area entered
void Kick::on_hitbox_entered (Node* current)
{
    //may use later
}

//reverse direction velocity 
void Kick::reverse_direction(Vector2 Wall)
{
    //reverse the direction 
    velocity=velocity.bounce(Wall);
    //three quarters the velocity 
    velocity=velocity*0.75;
    //stop animation 
    stop=true;
}
//animation spin 
//spin 
void Kick::animation_spin(float velocity)
{
    //simply add to rotation 
    set_rotation(get_rotation() + velocity);
}

//stop: used when walls are hit 
void Kick::animation_stop(bool stop)
{
    if(!stop)
    {
        //stop rotation
        set_rotation(get_rotation());
        //change stop variable
        stop=!stop;   
    }
    else
    {
        //spin
        animation_spin(velocity.length()/4);
        //change stop variable
        stop=!stop;
    }
}


//set kick force
void Kick ::set_kickForce(int value)
{
    kickForce=value;
}
//get kick force
int Kick ::get_kickForce() const 
{
    return kickForce;
}
//get and set kick force
//set damage
void Kick ::set_damage(int value)
{
    damage=value;
}
//get damage
int Kick ::get_damage() const {
    return damage;
}
//get and set velocity
Vector2 Kick ::get_velocity() const
{
    return velocity;
}
void Kick ::set_velocity(Vector2 v)
{
    velocity=v;
}