#ifndef REGISTER_TYPES_H
#define REGISTER_TYPES_H

#include <godot_cpp/core/class_db.hpp>

using namespace godot;

// These declarations must match the definitions in register_types.cpp
void initialize_kick_module(ModuleInitializationLevel p_level);
void uninitialize_kick_module(ModuleInitializationLevel p_level);

#endif