#ifndef PARTICLE_H

#define PARTICLE_H

#include "GameObject_Scene.h"

class Particle : public GameObject_Scene
{
public:
	int direction;
	int speed;
};

typedef Particle * Particle_ptr;

#endif