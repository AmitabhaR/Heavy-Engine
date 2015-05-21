package jruntime;

import jruntime.CollisionHandler;
import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class Scene implements Runnable , Cloneable
{
    ArrayList object_array;
    JPanel canvas;        
    public int A = 0,R = 0,G = 0,B = 0;
    public int speed = 1;
    public int gravity = 0;
    public String name = "";
    private boolean isRunning = false;
    public ArrayList collision_handlers;	
    DrawableGameObject[] sortedArray;
    
    class DrawableGameObject
    {
        int depth;
        int index;
    }
    
    public Scene( )
    {
        object_array = new ArrayList( );
        collision_handlers = new ArrayList( );
        sortedArray = new DrawableGameObject[0];
    }
    
    public void startScene(JPanel canvas)
    {
         this.canvas = canvas;
         isRunning = true;
         makeSorting( );
         
	 Thread t = new Thread(this);
         t.start();
    }
    
    @Override
    public void run( )
    {
        while(isRunning)
        {
            updateScene( );
            
            try
            {
                Thread.sleep(speed);
            }
            catch(InterruptedException ax)
            {
                
            }
        }
    }
		
		private void updateScene()
		{
                   boolean hasDeleted = false,hasDel = false; 
                    
                   do
                   {
                       hasDeleted = false;
                       
                       for(int cnt = 0;cnt < object_array.size();cnt++)
                       {
                           if (((GameObject_Scene) object_array.get(cnt)).isDestroyed)
                           {
                               object_array.remove(cnt);
                               hasDeleted = true;
                               hasDel = true;
                               break;
                           }
                       }
                       
                   } while(hasDeleted);
                   
                   if (hasDel) makeSorting( );
                   
                   
		   for (int cnt = 0;cnt < object_array.size();cnt++)
                   {
			     if (((GameObject_Scene) object_array.get(cnt)).obj_instance._static == false)
				 {
					if (((GameObject_Scene) object_array.get(cnt)).obj_instance.physics)
					{
						if (((GameObject_Scene) object_array.get(cnt)).obj_instance.rigidbody)
						{
							((GameObject_Scene) object_array.get(cnt)).Translate(0,gravity);
						}
						
						for(int cnt0 = 0;cnt0 < object_array.size( );cnt0++)
						{
						      if (cnt0 == cnt)
							  {
							    continue;
							  }
							  else
						      {
							    if (((GameObject_Scene) object_array.get(cnt)).obj_instance.img != null && ((GameObject_Scene) object_array.get(cnt0)).obj_instance.img != null && ((GameObject_Scene) object_array.get(cnt)).obj_instance.physics && ((GameObject_Scene) object_array.get(cnt0)).obj_instance.physics && ((GameObject_Scene) object_array.get(cnt)).obj_instance.collider && ((GameObject_Scene) object_array.get(cnt0)).obj_instance.collider)
								{
									if (((GameObject_Scene) object_array.get(cnt)).pos_x + ((GameObject_Scene) object_array.get(cnt)).obj_instance.img.getWidth( ) > ((GameObject_Scene) object_array.get(cnt0)).pos_x && ((GameObject_Scene) object_array.get(cnt)).pos_x < ((GameObject_Scene) object_array.get(cnt0)).pos_x + ((GameObject_Scene) object_array.get(cnt0)).obj_instance.img.getWidth( ) && ((GameObject_Scene) object_array.get(cnt)).pos_y + ((GameObject_Scene) object_array.get(cnt)).obj_instance.img.getHeight( ) > ((GameObject_Scene) object_array.get(cnt0)).pos_y && ((GameObject_Scene) object_array.get(cnt)).pos_y < ((GameObject_Scene) object_array.get(cnt0)).pos_y + ((GameObject_Scene) object_array.get(cnt0)).obj_instance.img.getHeight( ))
									{
                                                                            
                                                                            for(int c = 0;c < collision_handlers.size( );c++)
                                                                            {
                                                                                ((CollisionHandler) collision_handlers.get(c)).onCollision((GameObject_Scene) object_array.get(cnt),(GameObject_Scene) object_array.get(cnt0));
                                                                            }
									}
								}
							  }
						}
					}
                                 }
                       
                             ((GameObject_Scene) object_array.get(cnt)).processScripts();
		   }	
                   
                   NavigationManager.updateNavigation();
                   AnimationManager.updateAnimation();
		}
		
                 public boolean checkSorted(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.length; cnt++)
            {
                if (cnt + 1 < index_array.length)
                {
                    if (index_array[cnt].depth < index_array[cnt + 1].depth)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void sortElements(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.length; cnt++)
            {
                for (int c = cnt + 1; c < index_array.length; c++)
                {
                    if (index_array[cnt].depth < index_array[c].depth)
                    {
                        DrawableGameObject cp = index_array[cnt];

                        index_array[cnt] = index_array[c];

                        index_array[c] = cp;

                        break;
                    }
                }
            }
        }

        private void makeSorting()
        {
            sortedArray = new DrawableGameObject[object_array.size()];

            for (int cnt = 0; cnt < object_array.size( ); cnt++)
            {
                sortedArray[cnt] = new DrawableGameObject( );
                sortedArray[cnt].depth = ((GameObject_Scene) object_array.get(cnt)).depth;
                sortedArray[cnt].index = cnt;
            }

            while (!checkSorted(sortedArray))
            {
                sortElements(sortedArray);
            }
        }
                
		public void drawScene(Graphics g)
		{
                    g.setColor(new Color(R,G,B,A));
		    g.fillRect(0,0,canvas.getWidth(),canvas.getHeight());
			
		    for(int cnt = 0;cnt < object_array.size();cnt++)
			{
               if (((GameObject_Scene) object_array.get(cnt)).obj_instance.img != null)
			   {
                               
                   g.drawImage(((GameObject_Scene) object_array.get(cnt)).obj_instance.img,((GameObject_Scene) object_array.get(cnt)).pos_x,((GameObject_Scene) object_array.get(cnt)).pos_y,null);				   
			   }			   
			   else if (((GameObject_Scene) object_array.get(cnt)).obj_instance.text != "")
			   {
                               g.setFont(new Font(((GameObject_Scene) object_array.get(cnt)).obj_instance.font_name,Font.PLAIN,((GameObject_Scene) object_array.get(cnt)).obj_instance.font_size));
                               g.setColor(((GameObject_Scene)  object_array.get(cnt)).obj_instance.color);
                  g.drawString(((GameObject_Scene) object_array.get(cnt)).obj_instance.text,((GameObject_Scene) object_array.get(cnt)).pos_x,((GameObject_Scene) object_array.get(cnt)).pos_y);				  
			   }
			}
		}
		
		public void endScene( )
		{
		 isRunning = false;	
		}
		
		public boolean loadGameObject(GameObject_Scene gameObject)
		{
                   if (gameObject.instance_name == "")
                   {
                       return false;
                   } 
                   
                   for(int cnt = 0;cnt < object_array.size();cnt++)
                   {
                       if (((GameObject_Scene) object_array.get(cnt)).instance_name == gameObject.instance_name)
                       {
                           return false;
                       }
                   }
                   
                   object_array.add(gameObject);
                   makeSorting( );
                   
                   return true;
		}
		
                public void destroyGameObject(String instance_name )
                { 
                    for(int cnt = 0;cnt < object_array.size( );cnt++)
                    {
                        if (((GameObject_Scene) object_array.get(cnt)).instance_name == instance_name)
                        {
                            ((GameObject_Scene) object_array.get(cnt)).isDestroyed = true;
                            return;
                        }
                    }
                }
                
		public GameObject_Scene findGameObject(String name)
		{
			for(int cnt = 0;cnt < object_array.size( );cnt++)
			{
                            if ( ((GameObject_Scene) object_array.get(cnt)).instance_name == name)
			   {
					return (GameObject_Scene) object_array.get(cnt);
			   }			   
			}
			
			return null;
		}
                
                 public  ArrayList<GameObject> findGameObject(int tag)
           {
               ArrayList<GameObject> ret_list = new ArrayList<GameObject>( );
               
          for(int cnt = 0;cnt < object_array.size( );cnt++)
          {
		      if (((GameObject_Scene) object_array.get(cnt)).obj_instance.tag == tag)
			  {
                              GameObject null_obj = new GameObject( );
                              
                              null_obj.name = ((GameObject_Scene) object_array.get(cnt)).obj_instance.name;
                              null_obj.tag = ((GameObject_Scene) object_array.get(cnt)).obj_instance.tag;
                              null_obj.text =  ((GameObject_Scene)  object_array.get(cnt)).obj_instance.text;
                              null_obj.img = ((GameObject_Scene) object_array.get(cnt)).obj_instance.img;
                              null_obj._static = ((GameObject_Scene)  object_array.get(cnt)).obj_instance._static;
                              null_obj.collider = ((GameObject_Scene) object_array.get(cnt)).obj_instance.collider;
                              null_obj.color  = ((GameObject_Scene) object_array.get(cnt)).obj_instance.color;
                              null_obj.rigidbody = ((GameObject_Scene) object_array.get(cnt)).obj_instance.rigidbody;
                              null_obj.physics = ((GameObject_Scene) object_array.get(cnt)).obj_instance.physics;
                              null_obj.font_name = ((GameObject_Scene) object_array.get(cnt)).obj_instance.font_name;
                              null_obj.font_size = ((GameObject_Scene) object_array.get(cnt)).obj_instance.font_size;
                              
                              ret_list.add(null_obj);
			  }
		  }		  
		  
		  return ret_list;
	   }
                
                public void registerCollisionHandler(CollisionHandler handle)
                {
                    collision_handlers.add(handle);
                }
                
                @Override
                protected Object clone( ) throws CloneNotSupportedException
                {
                    return super.clone();
                }
	}
