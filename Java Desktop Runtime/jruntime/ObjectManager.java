/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.io.IOException;
import java.util.*;
import javax.microedition.lcdui.Image;

/**
 *
 * @author Riju
 */
public class ObjectManager 
{
    static Vector gameObject_array = new Vector( );

        public static void loadObject(String name,String text,String img_path,int tag,boolean isStatic,boolean isPhysics,boolean isRigid,boolean isCollider)
        {
	      GameObject instance = new GameObject( );
		  
		  instance.name = name;
		  instance.text = text;
                  
                  try
                  {  
                        instance.img = Image.createImage(img_path);
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
		  
                  gameObject_array.addElement(instance);
	   }
	   
	   public static Vector findGameObjectWithTag(int tag)
           {
               Vector ret_vec = new Vector( );
               
          for(int cnt = 0;cnt < gameObject_array.size( );cnt++)
          {
		      if (((GameObject) gameObject_array.elementAt(cnt)).tag == tag)
			  {
                              GameObject null_obj = new GameObject( );
                              
                              null_obj.name = ((GameObject) gameObject_array.elementAt(cnt)).name;
                              null_obj.tag = ((GameObject) gameObject_array.elementAt(cnt)).tag;
                              null_obj.text =  ((GameObject)  gameObject_array.elementAt(cnt)).text;
                              null_obj.img = ((GameObject) gameObject_array.elementAt(cnt)).img;
                              null_obj._static = ((GameObject)  gameObject_array.elementAt(cnt))._static;
                              null_obj.collider = ((GameObject) gameObject_array.elementAt(cnt)).collider;
                              null_obj.txt_R  = ((GameObject) gameObject_array.elementAt(cnt)).txt_R;
                              null_obj.txt_G  = ((GameObject) gameObject_array.elementAt(cnt)).txt_G;
                              null_obj.txt_B = ((GameObject) gameObject_array.elementAt(cnt)).txt_B;
                              null_obj.rigidbody = ((GameObject) gameObject_array.elementAt(cnt)).rigidbody;
                              null_obj.physics = ((GameObject) gameObject_array.elementAt(cnt)).physics;
                              null_obj.font = ((GameObject) gameObject_array.elementAt(cnt)).font;
                              
                              ret_vec.addElement(null_obj);		  
			  }
		  }		  
		  
		  return ret_vec;
	   }
	   
	   public static GameObject findGameObjectWithName(String name)
	   {	   
		  for(int cnt = 0;cnt < gameObject_array.size( );cnt++)
          {
		      if (((GameObject) gameObject_array.elementAt(cnt)).name == name)
			  {
                              GameObject null_obj = new GameObject( );
                              
                              null_obj.name = ((GameObject) gameObject_array.elementAt(cnt)).name;
                              null_obj.tag = ((GameObject) gameObject_array.elementAt(cnt)).tag;
                              null_obj.text =  ((GameObject)  gameObject_array.elementAt(cnt)).text;
                              null_obj.img = ((GameObject) gameObject_array.elementAt(cnt)).img;
                              null_obj._static = ((GameObject)  gameObject_array.elementAt(cnt))._static;
                              null_obj.collider = ((GameObject) gameObject_array.elementAt(cnt)).collider;
                              null_obj.txt_R  = ((GameObject) gameObject_array.elementAt(cnt)).txt_R;
                              null_obj.txt_G  = ((GameObject) gameObject_array.elementAt(cnt)).txt_G;
                              null_obj.txt_B = ((GameObject) gameObject_array.elementAt(cnt)).txt_B;
                              null_obj.rigidbody = ((GameObject) gameObject_array.elementAt(cnt)).rigidbody;
                              null_obj.physics = ((GameObject) gameObject_array.elementAt(cnt)).physics;
                              null_obj.font = ((GameObject) gameObject_array.elementAt(cnt)).font;
                              
                  return null_obj;			  
			  }
		  }		  
		  
		  return null;
	   }
}
