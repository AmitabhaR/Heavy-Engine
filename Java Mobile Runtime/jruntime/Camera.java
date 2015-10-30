/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.util.*;

/**
 *
 * @author Riju
 */
public class Camera 
{
     static Vector2 camera_pos;
     static float camera_rotation;

     public static void TranslateCamera(Vector2 pos)
     {
            camera_pos.x += pos.x;
            camera_pos.y += pos.y;
            
            Vector gameObject_list = HApplication.getActiveScene().getAllGameObject();
            
            for(int cntr = 0;cntr < gameObject_list.size();cntr++)
                if (((GameObject_Scene) gameObject_list.elementAt(cntr)).AllowCameraTranslation) ((GameObject_Scene) gameObject_list.elementAt(cntr)).Translate(-pos.x, -pos.y);
            
            NavigationManager.updateNavigatorTargets(pos);
     }

     public static void RotateCamera(double rotate_angle)
     {
         camera_rotation += rotate_angle;
         
         Vector gameObject_list = HApplication.getActiveScene().getAllGameObject();
            
            for(int cntr = 0;cntr < gameObject_list.size();cntr++)
                if (((GameObject_Scene) gameObject_list.elementAt(cntr)).AllowCameraRotation) ((GameObject_Scene) gameObject_list.elementAt(cntr)).Rotate(-rotate_angle);
     }

     public static Vector2 getCameraPosition() { return camera_pos;  }
     public static float getCameraRotation() { return camera_rotation; }
}
