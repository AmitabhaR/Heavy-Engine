/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

/**
 *
 * @author Riju
 */

import java.util.*;

  public class ParticleSystem
  {
        ArrayList particle_list;
        Scene sceneHandle;
        
        public ParticleSystem( Scene sceneHandle )
        {
            particle_list = new ArrayList();
            this.sceneHandle = sceneHandle;
        }

        public void addParticle(Particle particle)
        {
            while (!sceneHandle.loadGameObject(particle))
            {
                particle.instance_name += ( (particle.pos_y + 10) + (new Random()).nextInt(particle.pos_y * 10) + (particle.pos_x + 10) + (new Random()).nextInt(particle.pos_x * 10));
            }

            particle_list.add(particle);
        }

        public void addParticle(String instance_name, String object_name, int pos_x, int pos_y,int direction,int speed)
        {
            Particle particle = new Particle();

            particle.instance_name = "@XZX_PARTICLE_" + ( (particle.pos_y + 10) + (new Random()).nextInt(particle.pos_y * 10) + (particle.pos_x + 10) + (new Random()).nextInt(particle.pos_x * 10)); 
            particle.obj_instance = ObjectManager.findGameObjectWithName(object_name);
            particle.pos_x = pos_x;
            particle.pos_y = pos_y;
            particle.direction = direction;
            particle.speed = speed;

            while (!sceneHandle.loadGameObject(particle))
            {
                particle.instance_name += ( (particle.pos_y + 10) + (new Random()).nextInt(particle.pos_y * 10) + (particle.pos_x + 10) + (new Random()).nextInt(particle.pos_x * 10));
            }

            particle_list.add( particle );  
        }

        public void updateSystem()
        {
            boolean hasDeleted = false;
            
            do
            {
                hasDeleted = false;
                
            for (int cnt = 0; cnt < particle_list.size(); cnt++)
            {
                if ( ((Particle) particle_list.get(cnt)).pos_x < 0 || ((Particle) particle_list.get(cnt)).pos_x > HApplication.getWindowHandle().getWidth( ) || ((Particle) particle_list.get(cnt)).pos_y < 0 || ((Particle) particle_list.get(cnt)).pos_y > HApplication.getWindowHandle().getHeight( ))
                {
                    sceneHandle.destroyGameObject(((Particle) particle_list.get(cnt)).instance_name);
                    particle_list.remove(cnt);
                    hasDeleted = true;
                    break;
                }
                else
                {
                    if (((Particle) particle_list.get(cnt)).scriptsEmpty( ))
                    {
                        if (((Particle) particle_list.get(cnt)).direction == 1)
                        {
                            ((Particle) particle_list.get(cnt)).pos_y -= ((Particle) particle_list.get(cnt)).speed; // Up
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 2)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x -= ((Particle) particle_list.get(cnt)).speed; // Left
                            ((Particle) particle_list.get(cnt)).pos_y -= ((Particle) particle_list.get(cnt)).speed; // Up
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 3)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x -= ((Particle) particle_list.get(cnt)).speed; // Left
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 4)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x -= ((Particle) particle_list.get(cnt)).speed; // Left
                            ((Particle) particle_list.get(cnt)).pos_y += ((Particle) particle_list.get(cnt)).speed; // Down 
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 5)
                        {
                            ((Particle) particle_list.get(cnt)).pos_y += ((Particle) particle_list.get(cnt)).speed; // Down
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 6)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x += ((Particle) particle_list.get(cnt)).speed; // Right
                            ((Particle) particle_list.get(cnt)).pos_y += ((Particle) particle_list.get(cnt)).speed; // Down
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 7)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x += ((Particle) particle_list.get(cnt)).speed; // Right
                        }
                        else if (((Particle) particle_list.get(cnt)).direction == 8)
                        {
                            ((Particle) particle_list.get(cnt)).pos_x += ((Particle) particle_list.get(cnt)).speed; // Right
                            ((Particle) particle_list.get(cnt)).pos_y -= ((Particle) particle_list.get(cnt)).speed; // Up.
                        }
                    }
                }
            }
            
            } while (hasDeleted);
        }
 }
