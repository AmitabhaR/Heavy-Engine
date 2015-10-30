#include "ParticleSystem.h"
#include "ObjectManager.h"
#include "HApplication.h"

ParticleSystem::ParticleSystem(Scene_ptr scene)
{
	this->sceneHandle = scene;
}

void ParticleSystem::addParticle(Particle * particle_ptr)
{
	while (!sceneHandle->loadGameObject(particle_ptr))
	{
		particle_ptr->instance_name += ((particle_ptr->pos_x + 10) + rand() % (particle_ptr->pos_x * 10)) + ((particle_ptr->pos_y + 10) + rand() % (particle_ptr->pos_y * 10));
	}

	particle_list.push_back(particle_ptr);
}

void ParticleSystem::addParticle(std::string instance_name, std::string object_name, int pos_x, int pos_y, int direction, int speed)
{
	Particle * particle_ptr = new Particle();

	particle_ptr->instance_name = "@XZX_PARTICLE_" + ((particle_ptr->pos_x + 10) + rand() % (particle_ptr->pos_x * 10)) + ((particle_ptr->pos_y + 10) + rand() % (particle_ptr->pos_y * 10)) + instance_name;
	particle_ptr->obj_instance = ObjectManager::findGameObjectWithName(object_name);
	particle_ptr->pos_x = pos_x;
	particle_ptr->pos_y = pos_y;
	particle_ptr->direction = direction;
	particle_ptr->speed = speed;

	while (!sceneHandle->loadGameObject(particle_ptr))
	{
		particle_ptr->instance_name += ((particle_ptr->pos_x + 10) + rand() % (particle_ptr->pos_x * 10)) + ((particle_ptr->pos_y + 10) + rand() % (particle_ptr->pos_y * 10));
	}

	particle_list.push_back(particle_ptr);
}

void ParticleSystem::updateSystem()
{
re:

	for (int cnt = 0; cnt < particle_list.size( ); cnt++)
	{
		Particle_ptr particle_ptr = NULL;
		int cntr = 0;

		for (std::list<Particle_ptr>::iterator cur_particle = particle_list.begin(); cur_particle != particle_list.end(); cur_particle++,cntr++)
		{
			if (cnt == cntr)
			{
				particle_ptr = *cur_particle;
			}
		}

		SDL_Rect base_rect;

		SDL_GetWindowSize(HApplication::getWindowHandle(), &base_rect.w, &base_rect.h);

		if (particle_ptr->pos_x < 0 || particle_ptr->pos_x > base_rect.w || particle_ptr->pos_y < 0 || particle_ptr->pos_y > base_rect.h)
		{
			sceneHandle->destroyGameObject(particle_ptr->instance_name);

			cntr = 0;

			for (std::list<Particle_ptr>::iterator cur_particle = particle_list.begin(); cur_particle != particle_list.end(); cur_particle++, cntr++)
			{
				if (cnt == cntr)
				{
					particle_list.erase(cur_particle);
				}
			}

			goto re;
		}
		else
		{
			if (particle_ptr->scriptsEmpty())
			{
				if (particle_ptr->direction == 1)
				{
					particle_ptr->pos_y -= particle_ptr->speed; // Up
				}
				else if (particle_ptr->direction == 2)
				{
					particle_ptr->pos_x -= particle_ptr->speed; // Left
					particle_ptr->pos_y -= particle_ptr->speed; // Up
				}
				else if (particle_ptr->direction == 3)
				{
					particle_ptr->pos_x -= particle_ptr->speed; // Left
				}
				else if (particle_ptr->direction == 4)
				{
					particle_ptr->pos_x -= particle_ptr->speed; // Left
					particle_ptr->pos_y += particle_ptr->speed; // Down 
				}
				else if (particle_ptr->direction == 5)
				{
					particle_ptr->pos_y += particle_ptr->speed; // Down
				}
				else if (particle_ptr->direction == 6)
				{
					particle_ptr->pos_x += particle_ptr->speed; // Right
					particle_ptr->pos_y += particle_ptr->speed; // Down
				}
				else if (particle_ptr->direction == 7)
				{
					particle_ptr->pos_x += particle_ptr->speed; // Right
				}
				else if (particle_ptr->direction == 8)
				{
					particle_ptr->pos_x += particle_ptr->speed; // Right
					particle_ptr->pos_y -= particle_ptr->speed; // Up.
				}
			}
		}
	}
}