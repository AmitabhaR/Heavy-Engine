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
    public boolean AllowCameraTranslation = false;
    public boolean AllowCameraRotation = true;
    public int depth;
    public boolean Visibility = true;
    public int CollisionRectX = 0,CollisionRectY = 0;
    private double rotate_angle;
    private int scale_rate;
    private Image source_img;
    private boolean isChildReady = false;
    private Vector child_list;
    
    public GameObject_Scene()
    {
        scripts = new Vector( );
        child_list = new Vector( );
    }
    
    public void Initialize()
    {
        source_img = obj_instance.img;
    }
    
     private void LoadAllChilds( GameObject_Scene gameObject )
        {
            for (int cnt = 0; cnt < gameObject.child_list.size( );cnt++ )
            {
                gameObject.child_list.setElementAt(HApplication.getActiveScene().findGameObject(((GameObject_Scene) gameObject.child_list.elementAt(cnt)).instance_name),cnt);
                LoadAllChilds((GameObject_Scene) gameObject.child_list.elementAt(cnt));
            }

            gameObject.isChildReady = true;
        }

        public GameObject_Scene FindChildWithName( String child_name )
        {
            for(int cnt = 0;cnt < child_list.size( );cnt++)
            {
                if (((GameObject_Scene)child_list.elementAt(cnt)).instance_name == child_name) return (GameObject_Scene) child_list.elementAt(cnt);
            }

            return null;
        }

        public Vector FindChildWithTag( int tag )
        {
            Vector gameObject_list = new Vector();

            for(int cnt = 0;cnt < child_list.size( );cnt++)
            {
                if (((GameObject_Scene) child_list.elementAt(cnt)).obj_instance.tag == tag) gameObject_list.addElement(child_list.elementAt(cnt));
            }

            return (gameObject_list.size( ) > 0) ? gameObject_list : null;
        }

        public void UpdateChildPosition( int rate_x , int rate_y , boolean isParent )
        {
            if (!isParent)
            {
                pos_x += rate_x;
                pos_y += rate_y;
            }

            for(int cnt = 0;cnt < child_list.size( );cnt++)
            {
                ((GameObject_Scene)child_list.elementAt(cnt)).UpdateChildPosition(rate_x, rate_y, false);
            }
        }

        public void UpdateChildRotation(double rate_angle, boolean isParent)
        {
            if (!isParent)
            {
                applyRotation((int) (rate_angle + rotate_angle));
                rotate_angle += rate_angle;
            }

            for(int cnt = 0;cnt < child_list.size( );cnt++)
            {
                GameObject_Scene gameObj = (GameObject_Scene) child_list.elementAt(cnt);
                double rad_angle = Math.PI * rate_angle / 180;
                double def_x = gameObj.pos_x - (pos_x + obj_instance.img.getWidth( ) / 2);
                double def_y = gameObj.pos_y - (pos_y + obj_instance.img.getHeight( ) / 2);

                gameObj.pos_x +=(int) Math.cos(rad_angle) * def_x - Math.sin(rad_angle) * def_y;
                gameObj.pos_y +=(int) Math.sin(rad_angle) * def_x + Math.cos(rad_angle) * def_y;

                gameObj.UpdateChildRotation(rate_angle, false);
            }
        }

        public void UpdateChildScale( float rate_scale , boolean isParent )
        {
            if (!isParent) scale_rate += rate_scale;

            for(int cnt = 0;cnt < child_list.size( );cnt++)
            {
                ((GameObject_Scene) child_list.elementAt(cnt)).UpdateChildScale(rate_scale, false);
            }
        }

        public void AddChild(String child_name)
        {
            GameObject_Scene gameObj = new GameObject_Scene();

            gameObj.instance_name = child_name;

            child_list.addElement(gameObj);
        }
    
    public void SetScale(int scale)
    {
        if (scale > 0) scale_rate = scale;
        if (!isChildReady) LoadAllChilds( this );
        UpdateChildScale(scale,true);
    }
    
    public int GetScale()
    {
        return scale_rate;
    }
    
    public void SetRotationAngle(double angle)
    {
        double prev_angle = rotate_angle;
        applyRotation(angle);
        rotate_angle = angle;
        
        if (!isChildReady) LoadAllChilds( this );
        UpdateChildRotation(angle - prev_angle,true);
    }
    
    public double GetRotationAngle()
    {
        return rotate_angle;
    }
    
    private void applyRotation(double angle)
    {
        int sw = source_img.getWidth();
        int sh = source_img.getHeight();
        int[] srcData = new int[sw * sh];
     
        source_img.getRGB(srcData, 0, sw, 0, 0, sw, sh);
        int[] dstData = new int[sw * sh];
     
        double rads = angle / 60;
        double sa = Math.sin(rads);
        double ca = Math.cos(rads);
        double isa = (256 * sa);
        double ica = (256 * ca);
     
        int my = - (sh >> 1);
        for(int i = 0; i < sh; i++) {
            int wpos = i * sw;
     
            int xacc = (int) ( my * isa - (sw >> 1) * ica + ((sw >> 1) << 8) );
            int yacc = (int) ( my * ica + (sw >> 1) * isa + ((sh >> 1) << 8) );
     
            for(int j = 0; j < sw; j++) {
                int srcx = (xacc >> 8);
                int srcy = (yacc >> 8);
     
                if(srcx < 0) srcx = 0;
                if(srcy < 0) srcy = 0;
                if(srcx > sw - 1) srcx = sw - 1;
                if(srcy > sh - 1) srcy = sh - 1;
     
                dstData[wpos++] = srcData[srcx + srcy * sw];
     
                xacc += ica;
                yacc -= isa;
            }
            my++;
        }
     
        obj_instance.img.getGraphics().drawRGB(dstData, 0, sw, 0, 0, sw, sh, true);
    }
    
    public void Rotate(double angle)
    {
        applyRotation(rotate_angle + angle);
        rotate_angle += angle;
        
        if (!isChildReady) LoadAllChilds( this );
        UpdateChildRotation(angle,true);
    }
    
    public void Translate(int x , int y)
    {
          pos_x += x;
          pos_y += y;
          
          if (!isChildReady) LoadAllChilds( this );
          UpdateChildPosition(x,y,true);
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
