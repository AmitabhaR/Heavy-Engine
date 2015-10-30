/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.util.*;
import javax.microedition.lcdui.*;
import javax.microedition.lcdui.game.*;
/**
 *
 * @author Riju
 */
public class Scene extends GameCanvas implements CommandListener,Runnable
{
    Vector object_array;  
    public int A = 0,R = 0,G = 0,B = 0;
    public int speed = 1;
    public int gravity = 0;
    public String name = "";
    private boolean isRunning = false;
    private Vector collision_handlers;	
    private Vector touch_handlers;
    DrawableGameObject[] sortedArray;
    Command exit_com;
    
    class DrawableGameObject
    {
        int depth;
        int index;
    }
    
    public Scene( )
    {
        super(false);
        object_array = new Vector( );
        collision_handlers = new Vector( );
        touch_handlers = new Vector( );
        sortedArray = new DrawableGameObject[0];
        exit_com = new Command("Exit",Command.BACK,0);
        
        this.addCommand(exit_com);
        this.setCommandListener(this);
    }
    
    public void startScene()
    {
         isRunning = true;
         makeSorting( );
         
	 Thread t = new Thread(this);
         t.start();
    }
    
    private Image resizeImage(Image img , int width , int height)
    {
        int[] rawInput = new int[img.getHeight() * img.getWidth()];
        img.getRGB(rawInput, 0, img.getWidth(), 0, 0, img.getWidth(), img.getHeight());

        int[] rawOutput = new int[width * height];

        // YD compensates for the x loop by subtracting the width back out
        int YD = (img.getHeight() / height) * img.getWidth() - img.getWidth();
        int YR = img.getHeight() % height;
        int XD = img.getWidth() / width;
        int XR = img.getWidth() % width;
        int outOffset = 0;
        int inOffset = 0;

        for (int y = height, YE = 0; y > 0; y--) {
          for (int x = width, XE = 0; x > 0; x--) {
            rawOutput[outOffset++] = rawInput[inOffset];
            inOffset += XD;
            XE += XR;
            if (XE >= width) {
              XE -= width;
              inOffset++;
            }
          }
          inOffset += YD;
          YE += YR;
          if (YE >= height) {
            YE -= height;
            inOffset += img.getWidth();
          }
        }
        rawInput = null;
        return Image.createRGBImage(rawOutput, width, height, true);
    }
    
    public void run( )
    {
        while(isRunning)
        {
            updateScene( );
            drawScene(getGraphics( ));
            flushGraphics( );
            
            try
            {
                Thread.sleep(speed);
            }
            catch(InterruptedException ax)
            {
                
            }
        }
    }
	
    public void commandAction(Command com,Displayable dis)
    {
        if (exit_com == com)
        {
          HApplication.exitApp();
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
                           if (((GameObject_Scene) object_array.elementAt(cnt)).isDestroyed)
                           {
                               object_array.removeElementAt(cnt);
                               hasDeleted = true;
                               hasDel = true;
                               break;
                           }
                       }
                       
                   } while(hasDeleted);
                   
                   if (hasDel) makeSorting( );
                   
                   
		   for (int cnt = 0;cnt < object_array.size();cnt++)
           {
			     if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance._static == false)
				 {
					if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.physics)
					{
						if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.rigidbody)
						{
							((GameObject_Scene) object_array.elementAt(cnt)).Translate(0,gravity);
						}
						
						for(int cnt0 = 0;cnt0 < object_array.size( );cnt0++)
						{
						      if (cnt0 == cnt)
							  {
							    continue;
							  }
							  else
						      {
							    if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img != null && ((GameObject_Scene) object_array.elementAt(cnt0)).obj_instance.img != null && ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.physics && ((GameObject_Scene) object_array.elementAt(cnt0)).obj_instance.physics && ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.collider && ((GameObject_Scene) object_array.elementAt(cnt0)).obj_instance.collider)
								{
									if (((GameObject_Scene) object_array.elementAt(cnt)).pos_x + ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img.getWidth( ) > ((GameObject_Scene) object_array.elementAt(cnt0)).pos_x && ((GameObject_Scene) object_array.elementAt(cnt)).pos_x < ((GameObject_Scene) object_array.elementAt(cnt0)).pos_x + ((GameObject_Scene) object_array.elementAt(cnt0)).obj_instance.img.getWidth( ) && ((GameObject_Scene) object_array.elementAt(cnt)).pos_y + ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img.getHeight( ) > ((GameObject_Scene) object_array.elementAt(cnt0)).pos_y && ((GameObject_Scene) object_array.elementAt(cnt)).pos_y < ((GameObject_Scene) object_array.elementAt(cnt0)).pos_y + ((GameObject_Scene) object_array.elementAt(cnt0)).obj_instance.img.getHeight( ) && ((GameObject_Scene) object_array.elementAt(cnt)).depth == ((GameObject_Scene) object_array.elementAt(cnt0)).depth)
									{
                                                                            
                                                                            for(int c = 0;c < collision_handlers.size( );c++)
                                                                            {
                                                                                ((CollisionHandler) collision_handlers.elementAt(c)).onCollision((GameObject_Scene) object_array.elementAt(cnt),(GameObject_Scene) object_array.elementAt(cnt0));
                                                                            }
									}
								}
							  }
						}
					}
                                 }
                           
                           ((GameObject_Scene) object_array.elementAt(cnt)).processScripts();
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
                sortedArray[cnt].depth = ((GameObject_Scene) object_array.elementAt(cnt)).depth;
                sortedArray[cnt].index = cnt;
            }

            while (!checkSorted(sortedArray))
            {
                sortElements(sortedArray);
            }
        }
        
        public Vector getAllGameObject() { return object_array; }
        
		public void drawScene(Graphics g)
		{
                    g.setColor(R,G,B);
		    g.fillRect(0,0,getWidth( ),getHeight( ));
			
		    for(int cnt = 0;cnt < object_array.size();cnt++)
			{
               if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img != null)
			   {
                               
                            g.drawImage(resizeImage(((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img,((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img.getWidth() + ((GameObject_Scene) object_array.elementAt(cnt)).GetScale(),((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img.getHeight() + ((GameObject_Scene) object_array.elementAt(cnt)).GetScale()),((GameObject_Scene) object_array.elementAt(cnt)).pos_x,((GameObject_Scene) object_array.elementAt(cnt)).pos_y,Graphics.TOP | Graphics.LEFT);				   
			   }			   
			   else if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.text != "")
			   {
                               g.setFont(((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.font);
                               g.setColor(((GameObject_Scene)  object_array.elementAt(cnt)).obj_instance.txt_R,((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.txt_G,((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.txt_B);
                               g.drawString(((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.text,((GameObject_Scene) object_array.elementAt(cnt)).pos_x,((GameObject_Scene) object_array.elementAt(cnt)).pos_y,Graphics.TOP | Graphics.LEFT);				  
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
                       if (((GameObject_Scene) object_array.elementAt(cnt)).instance_name == gameObject.instance_name)
                       {
                           return false;
                       }
                   }
                   
                    
                   object_array.addElement(gameObject);
                   makeSorting( );
                   
                   return true;
		}
		
                public void destroyGameObject(String instance_name )
                { 
                    for(int cnt = 0;cnt < object_array.size( );cnt++)
                    {
                        if (((GameObject_Scene) object_array.elementAt(cnt)).instance_name == instance_name)
                        {
                            ((GameObject_Scene) object_array.elementAt(cnt)).isDestroyed = true;
                            return;
                        }
                    }
                }
                
		public GameObject_Scene findGameObject(String name)
		{
			for(int cnt = 0;cnt < object_array.size( );cnt++)
			{
                            if ( ((GameObject_Scene) object_array.elementAt(cnt)).instance_name == name)
			   {
					return (GameObject_Scene) object_array.elementAt(cnt);
			   }			   
			}
			
			return null;
		}
                
           public Vector findGameObject(int tag)
           {
               Vector ret_list = new Vector( );
               
          for(int cnt = 0;cnt < object_array.size( );cnt++)
          {
		      if (((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.tag == tag)
			  {
                              GameObject null_obj = new GameObject( );
                              
                              null_obj.name = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.name;
                              null_obj.tag = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.tag;
                              null_obj.text =  ((GameObject_Scene)  object_array.elementAt(cnt)).obj_instance.text;
                              null_obj.img = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.img;
                              null_obj._static = ((GameObject_Scene)  object_array.elementAt(cnt)).obj_instance._static;
                              null_obj.collider = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.collider;
                              null_obj.txt_R  = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.txt_R;
                              null_obj.txt_G = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.txt_G;
                              null_obj.txt_B = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.txt_R;
                              null_obj.rigidbody = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.rigidbody;
                              null_obj.physics = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.physics;
                              null_obj.font = ((GameObject_Scene) object_array.elementAt(cnt)).obj_instance.font;
                              
                              ret_list.addElement(null_obj);
			  }
		  }		  
		  
		  return ret_list;
	   }
                
                public void registerCollisionHandler(CollisionHandler handle)
                {
                    collision_handlers.addElement(handle);
                }
                
                public void registerTouchHandler(TouchHandler handler)
                {
                    touch_handlers.addElement(handler);
                }
                
                  protected void pointerPressed(int x ,int y)
                 {
                     for(int cnt = 0;cnt < touch_handlers.size();cnt++)
                     {
                         ((TouchHandler) touch_handlers.elementAt(cnt)).onTouch(x, y);
                     }
                  }
      
                 protected void pointerDragged(int x,int y)
                {
                      for(int cnt = 0;cnt < touch_handlers.size();cnt++)
                     {
                         ((TouchHandler) touch_handlers.elementAt(cnt)).onTouchMoved(x, y);
                     }
                }
      
                protected void pointerReleased(int x , int y )
                {
                    for(int cnt = 0;cnt < touch_handlers.size();cnt++)
                     {
                         ((TouchHandler) touch_handlers.elementAt(cnt)).onTouchReleased(x, y);
                     }
                }
                
}
