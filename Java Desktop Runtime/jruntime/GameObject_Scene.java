package jruntime;

import java.awt.*;
import java.awt.Color;
import java.awt.Font;
import java.awt.image.BufferedImage;
import java.util.ArrayList;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

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
    private ArrayList scripts;
    public boolean isDestroyed = false;
    public boolean AllowCameraTranslation = false;
    public boolean AllowCameraRotation = false;
    public int depth;
    private float rotation_angle;
    private float scale_rate;
    private BufferedImage source_img;
    private boolean isChildReady = false;
    private ArrayList<GameObject_Scene> child_list;
     
    public GameObject_Scene( )
    {
        scripts = new ArrayList( );
        child_list = new ArrayList<GameObject_Scene>( );
    }
    
    public void Initialize( )
    {
        source_img = obj_instance.img;
    }
    
      private void LoadAllChilds( GameObject_Scene gameObject )
        {
            for (int cnt = 0; cnt < gameObject.child_list.size( );cnt++ )
            {
                gameObject.child_list.set(cnt, HApplication.getActiveScene().findGameObject(gameObject.child_list.get(cnt).instance_name));
                LoadAllChilds(gameObject.child_list.get(cnt));
            }

            gameObject.isChildReady = true;
        }

        public GameObject_Scene FindChildWithName( String child_name )
        {
            for(GameObject_Scene gameObject : child_list)
            {
                if (gameObject.instance_name == child_name) return gameObject;
            }

            return null;
        }

        public ArrayList<GameObject_Scene> FindChildWithTag( int tag )
        {
            ArrayList<GameObject_Scene> gameObject_list = new ArrayList<GameObject_Scene>();

            for(GameObject_Scene gameObject : child_list)
            {
                if (gameObject.obj_instance.tag == tag) gameObject_list.add(gameObject);
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

            for(GameObject_Scene gameObj : child_list)
            {
                gameObj.UpdateChildPosition(rate_x, rate_y, false);
            }
        }

        public void UpdateChildRotation(float rate_angle, boolean isParent)
        {
            if (!isParent)
            {
                ApplyRotation(-(rate_angle + rotation_angle));
                rotation_angle += rate_angle;
            }

            for(GameObject_Scene gameObj : child_list)
            {
                double rad_angle = Math.PI * rate_angle / 180;
                double def_x = gameObj.pos_x - (pos_x + obj_instance.img.getWidth( ) / 2);
                double def_y = gameObj.pos_y - (pos_y + obj_instance.img.getHeight( ) / 2);

                gameObj.pos_x +=(int) Math.round( Math.cos(rad_angle) * def_x - Math.sin(rad_angle) * def_y );
                gameObj.pos_y +=(int) Math.round( Math.sin(rad_angle) * def_x + Math.cos(rad_angle) * def_y );

                gameObj.UpdateChildRotation(rate_angle, false);
            }
        }

        public void UpdateChildScale( float rate_scale , boolean isParent )
        {
            if (!isParent) scale_rate += rate_scale;

            for(GameObject_Scene gameObj : child_list)
            {
                gameObj.UpdateChildScale(rate_scale, false);
            }
        }

        public void AddChild(String child_name)
        {
            GameObject_Scene gameObj = new GameObject_Scene();

            gameObj.instance_name = child_name;

            child_list.add(gameObj);
        }
    
    public float GetScale()
    {
        return scale_rate;
    }
    
    public void SetScale(float scale)
    {
        if (scale_rate > 0) scale_rate = scale;
        
        if (!isChildReady) LoadAllChilds( this );
        this.UpdateChildScale(scale, true);
    }
            
    public float GetRotationAngle( )
    {
        return rotation_angle;
    }
    
    public void SetRotationAngle(float angle)
    {
        float prev_angle = rotation_angle;
        
        ApplyRotation(angle);
        rotation_angle = angle;
        
        if (!isChildReady) LoadAllChilds( this );
        this.UpdateChildRotation(angle - prev_angle, true);
    }
    
    private void ApplyRotation(float angle)
    {
        BufferedImage new_img = new BufferedImage(obj_instance.img.getWidth(),obj_instance.img.getHeight(),obj_instance.img.getType());
        Graphics2D g = (Graphics2D) new_img.getGraphics();
        
        g.rotate(Math.toRadians(angle), 0 , 0);
        g.drawImage(source_img,pos_x, pos_y , null);
        
        obj_instance.img = new_img;
    }
    
    public void Rotate(float angle)
    {
        ApplyRotation(angle + rotation_angle);
        rotation_angle += angle;
        
        
        if (!isChildReady) LoadAllChilds( this );
        this.UpdateChildRotation(angle, true);
    }
    
    public void Translate(int x , int y)
    {
          pos_x += x;
          pos_y += y;
          
          
        if (!isChildReady) LoadAllChilds( this );
        this.UpdateChildPosition(x,y, true);
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
		
    public void setImage(BufferedImage img)
    {
            obj_instance.img = img;
    }
        
    public void setColor(Color col)
    {
        obj_instance.color = col;
    }
    
    public void setFont(Font font)
    {
          obj_instance.font_name = font.getName();
          obj_instance.font_size = font.getSize( );
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
                   ((HeavyScript) scripts.get(c)).process(this);
             }
        }
    }
     
    public void registerScript(HeavyScript handle )
    {
         scripts.add(handle);
    }
}
