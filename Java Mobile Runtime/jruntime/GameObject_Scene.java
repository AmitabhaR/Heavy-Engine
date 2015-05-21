/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.util.*;
import javax.microedition.lcdui.*;
/**
 *
 * @author Riju
 */
public class GameObject_Scene 
{
    public int pos_x;
    public int pos_y;
    public GameObject obj_instance;
    public String instance_name;
    public Vector scripts;
    public boolean isDestroyed = false;
    public int depth;
    
    public GameObject_Scene()
    {
        scripts = new Vector( );
    }
    
    public void Translate(int x , int y)
    {
          pos_x += x;
          pos_y += y;
    }
    		
    public void setText(String text)
    {
            obj_instance.text = text;
    }
		
    public void setStatic(boolean value)
    {
            obj_instance._static = value;
    }
		
    public void setRigid(boolean value)
    {
            obj_instance.rigidbody = value;
    }
		
    public void setCollider(boolean value)
    {
            obj_instance.collider = value;
    }
		
    public void setTag(int tag)
    {
            obj_instance.tag = tag;
    }
		
    public void setImage(Image img)
    {
            obj_instance.img = img;
    }
        
    public void setColor(int R,int G,int B)
    {
        obj_instance.txt_R = R;
        obj_instance.txt_G = G;
        obj_instance.txt_B = B;
    }
    
    public void setFont(Font font)
    {
          obj_instance.font = font;
    }
    
    public boolean scriptsEmpty()
    {
         return (scripts.size( ) > 0) ? false : true;
    }
    
    public void processScripts( )
    {
        if (scripts.size() > 0)
        {             
             for(int c = 0;c < scripts.size( );c++)
             {
                   ((HeavyScript) scripts.elementAt(c)).process(this);
             }
        }
    }
    
    public void registerScript(HeavyScript handle )
    {
         scripts.addElement(handle);
    }
}
