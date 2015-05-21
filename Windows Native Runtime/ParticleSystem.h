#ifndef PARTICLE_SYSTEM_H

#define PARTICLE_SYSTEM_H

#include "Particle.h"
#include "Scene.h"
#include <list>

class ParticleSystem
{
private:
	std::list<Particle *> particle_list;
	Scene * sceneHandle;

public:

	ParticleSystem(Scene *);

	void addParticle(Particle *);

	void addParticle(std::string , std::string , int , int , int , int);

	void updateSystem();
};

#endif 