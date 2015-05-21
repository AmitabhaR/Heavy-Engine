package jruntime;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import javax.imageio.ImageIO;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
 public class ObjectLoader
{
       static ArrayList gameObject_array = new ArrayList( );

        public static void loadObject(String name,String text,String img_path,int tag,boolean isStatic,boolean isPhysics,boolean isRigid,boolean isCollider)
        {
	      GameObject instance = new GameObject( );
		  
		  instance.name = name;
		  instance.text = text;
                  
                  try
                  {  
                        instance.img = ImageIO.read( (new Object( )).getClass().getResource(img_path));
                   }
                   catch(IOException ax)
                   {
                    instance.img = null;      
                   }
                   catch(IllegalArgumentException cx)
                   {
                    instance.img = null;   
                   }
                  
		  instance.tag = tag;
		  instance._static = isStatic;
		  instance.physics = isPhysics;
		  instance.rigidbody = isRigid;
		  instance.collider = isCollider;
		  
                  gameObject_array.add(instance);
	   }
	   
	   public static ArrayList<GameObject> findGameObjectWithTag(int tag)
           {
               ArrayList<GameObject> ret_list = new ArrayList<GameObject>( );
               
          for(int cnt = 0;cnt < gameObject_array.size( );cnt++)
          {
		      if (((GameObject) gameObject_array.get(cnt)).tag == tag)
			  {
                              GameObject null_obj = new GameObject( );
                              
                              null_obj.name = ((GameObject) gameObject_array.get(cnt)).name;
                              null_obj.tag = ((GameObject) gameObject_array.get(cnt)).tag;
                              null_obj.text =  ((GameObject)  gameObject_array.get(cnt)).text;
                              null_obj.img = ((GameObject) gameObject_array.get(cnt)).img;
                              null_obj._static = ((GameObject)  gameObject_array.get(cnt))._static;
                              null_obj.collider = ((GameObject) gameObject_array.get(cnt)).collider;
                              null_obj.color  = ((GameObject) gameObject_array.get(cnt)).color;
                              null_obj.rigidbody = ((GameObject) gameObject_array.get(cnt)).rigidbody;
                              null_obj.physics = ((GameObject) gameObject_array.get(cnt)).physics;
                              null_obj.font_name = ((GameObject) gameObject_array.get(cnt)).font_name;
                              null_obj.font_size = ((GameObject) gameObject_array.get(cnt)).font_size;
                              
                              ret_list.add(null_obj);
			  }
		  }		  
		  
		  return ret_list;
	   }
	   
	   public static GameObject findGameObjectWithName(String name)
	   {	   
		  for(int cnt = 0;cnt < gameObject_array.size( );cnt++)
          {
		      if (((GameObject) gameObject_array.get(cnt)).name == name)
			  {
                              GameObject null_obj = new GameObject( );
                              
                               null_obj.name = ((GameObject) gameObject_array.get(cnt)).name;
                              null_obj.tag = ((GameObject) gameObject_array.get(cnt)).tag;
                              null_obj.text =  ((GameObject)  gameObject_array.get(cnt)).text;
                              null_obj.img = ((GameObject) gameObject_array.get(cnt)).img;
                              null_obj._static = ((GameObject)  gameObject_array.get(cnt))._static;
                              null_obj.collider = ((GameObject) gameObject_array.get(cnt)).collider;
                              null_obj.color  = ((GameObject) gameObject_array.get(cnt)).color;
                              null_obj.rigidbody = ((GameObject) gameObject_array.get(cnt)).rigidbody;
                              null_obj.physics = ((GameObject) gameObject_array.get(cnt)).physics;
                              null_obj.font_name = ((GameObject) gameObject_array.get(cnt)).font_name;
                              null_obj.font_size = ((GameObject) gameObject_array.get(cnt)).font_size;
                              
                  return null_obj;			  
			  }
		  }		  
		  
		  return null;
	   }
 }